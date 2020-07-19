using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiPrimerWebApiM3.Helpers
{
    /* Implementación de un filtro de acción */
    public class MiFiltroDeAccion : IActionFilter
    {
        private readonly ILogger<MiFiltroDeAccion> logger;
        public MiFiltroDeAccion(ILogger<MiFiltroDeAccion> logger)
        { 
            this.logger = logger; 
        }

        /* Se ejecuta después de la acción */
        public void OnActionExecuted(ActionExecutedContext context)
        {
            logger.LogError("OnActionExecutedContext");
        }

        /* Se ejecuta antes de la acción */
        public void OnActionExecuting(ActionExecutingContext context)
        {
            logger.LogError("ActionExecutingContext");
        }
    }
}
