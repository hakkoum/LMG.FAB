using LMG.Fab.Data.Entities.LmgFab;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMG.FAB.Services.FAB
{
    public  interface ITiersManager
    {
        List<Tiers> GetTiersList();

        Tiers GetTiers(int tiersId);

        List<Prestation> GetPrestationList();

        List<Prestation> GetTiersPrestations(int tiersId);


        Prestation GetPrestation(int prestationId);

        List<Machine> GetPrestationMachines(int prestationId);

    }
}
