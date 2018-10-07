using LMG.Fab.Data.Entities.LmgFab;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMG.FAB.Services.FAB
{
    public interface ILotManager
    {
        Lot GetLot(int lotId);

        Lot Save(Lot lot);

        void Delete(int lotId);

        List<VwLot> GetLotsList(string nomLot, string codeLot, DateTime? dateMiseEnVenteDebut, DateTime? dateMiseEnVentefin, int? processusId);

        (Boolean processOk, List<string> errors) SyncLotFromRef(DateTime minDate, string[] monthsNames);
    }
}
