using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using S_CIngenieria.Service;

namespace S_CIngenieria.Filters
{
    public class PermisosAttribute : ActionFilterAttribute
    {
        private readonly string _permisoRequerido;
        private readonly IUsuarioService _usuarioService;

        public PermisosAttribute(string permisoRequerido, IUsuarioService usuarioService)
        {
            _permisoRequerido = permisoRequerido;
            _usuarioService = usuarioService;
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var usuarioId = int.Parse(context.HttpContext.User.Identity.Name);
            var permisos = await _usuarioService.GetPermisosPorUsuario(usuarioId);


            bool tienePermiso = permisos.Any(p => p.GetType().GetProperty(_permisoRequerido)?.GetValue(p, null) as bool? == true);

            if (!tienePermiso)
            {
                context.Result = new ForbidResult();
                return;
            }

            await next();
        }
    }
}
