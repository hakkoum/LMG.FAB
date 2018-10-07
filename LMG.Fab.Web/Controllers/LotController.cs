using LMG.Fab.Data.Entities.LmgFab;
using LMG.FAB.Services.FAB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMG.Fab.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/lot")]
    [Authorize]
    public class LotController : Controller
    {
        private ILotManager _lotManager;

        public LotController(ILotManager lotManager)
        {
            _lotManager = lotManager;
        }

        [HttpGet]
        [Route("list")]
        public List<VwLot> GetLots(string nomLot, string codeLot, DateTime? dateMiseEnVenteDebut, DateTime? dateMiseEnVentefin, int? processusId)
        {
            return _lotManager.GetLotsList(nomLot, codeLot, dateMiseEnVenteDebut, dateMiseEnVentefin, processusId);
        }

        [HttpGet]
        [Route("getLot")]
        public Lot GetLot(int lotId)
        {
            return _lotManager.GetLot(lotId);
        }

        [HttpPost]
        [Route("saveLot")]
        public IActionResult SaveLot([FromBody]Lot lot)
        {
            if (lot.NomLot == "gg")
            {
                throw new Exception("test exception from server !!!");
            }

            var result = _lotManager.Save(lot);
            return Json(true);
        }
    }
}
