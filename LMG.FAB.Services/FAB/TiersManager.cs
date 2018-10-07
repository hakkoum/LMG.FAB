using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMG.Fab.Data.Entities.LmgFab;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LMG.FAB.Services.FAB
{
    public class TiersManager : ITiersManager
    {
        private LMG_FABContext _fabContext;
        private ILogger<LotManager> _logger;
        public TiersManager(LMG_FABContext context, ILogger<LotManager> logger)
        {
            _fabContext = context;
            _logger = logger;
        }


        public Prestation GetPrestation(int prestationId)
        {
            return _fabContext.Prestation.Find(prestationId);
        }

        public List<Prestation> GetPrestationList()
        {
            return _fabContext.Prestation.ToList();
        }

        public List<Machine> GetPrestationMachines(int prestationId)
        {
            return _fabContext.Machine.Where(p => p.FkPrestation == prestationId).ToList();
        }

        public Tiers GetTiers(int tiersId)
        {
            var tiers = _fabContext.Tiers.Include(p => p.Prestation).ThenInclude(p => p.Machine).FirstOrDefault(p => p.PkTiers == tiersId);

            return tiers;
        }

        public List<Tiers> GetTiersList()
        {
            var result = _fabContext.Tiers.Include(p => p.Prestation).ThenInclude(p => p.Machine).ToList();


            return result;
        }

        public List<Prestation> GetTiersPrestations(int tiersId)
        {
            return _fabContext.Prestation.Where(p => p.FkTiers == tiersId).ToList();
        }
    }
}
