using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMG.Fab.Data.Entities.LmgFab;
using LMG.Fab.Data.Entities.LmgFab.Custom;
using LMG.Fab.Data.Entities.Reflmg;
using LMG.FAB.Services.Business;
using LMG.FAB.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LMG.FAB.Services.FAB
{
    public class LotManager : ILotManager
    {
        private LMG_FABContext _fabContext;
        private ILockManager _lockManager;
        private ReflmgContext _refContext;
        private IDateFormater _dateFormater;
        private ILogger<LotManager> _logger;
        public LotManager(LMG_FABContext context, ILockManager lockManager, ReflmgContext refContext,
            IDateFormater dateFormater, ILogger<LotManager> logger)
        {
            _fabContext = context;
            _lockManager = lockManager;
            _refContext = refContext;
            _dateFormater = dateFormater;
            _logger = logger;
        }

        public void Delete(int lotId)
        {
            //change status to deleted
        }

        public Lot GetLot(int lotId)
        {
            return _fabContext.Lot.FirstOrDefault(p => p.PkLot == lotId);
        }

        public List<VwLot> GetLotsList(string nomLot, string codeLot, DateTime? dateMiseEnVenteDebut, DateTime? dateMiseEnVentefin, int? processusId)
        {
            var query = _fabContext.VwLot.AsQueryable();
            if (!string.IsNullOrWhiteSpace(nomLot))
            {
                query = query.Where(x => x.NomLot.Contains(nomLot));
            }
            if (!string.IsNullOrWhiteSpace(codeLot))
            {
                query = query.Where(x => x.CodeLot.Contains(codeLot));
            }
            if (dateMiseEnVenteDebut.HasValue)
            {
                query = query.Where(x => x.DateMiseEnVente >= dateMiseEnVenteDebut.Value);
            }
            if (dateMiseEnVentefin.HasValue)
            {
                query = query.Where(x => x.DateMiseEnVente <= dateMiseEnVentefin.Value);
            }
            if (processusId.HasValue)
            {
                query = query.Where(x => x.FkParamDetailProc == processusId.Value);
            }

            return query.ToList();
        }

        public Lot Save(Lot lot)
        {
            if (lot.PkLot > 0)
            {
                _fabContext.Lot.Update(lot);
                _fabContext.SaveChanges();
                _lockManager.DeleteTableLock("lot", lot.PkLot);
            }
            else
            {
                _fabContext.Lot.Add(lot);
                _fabContext.SaveChanges();
            }
            return lot;
        }

        public (bool processOk, List<string> errors) SyncLotFromRef(DateTime minDate, string[] monthsNames)
        {
            List<string> errors = new List<string>();
            try
            {
                var refOffices = _refContext.Offices.Where(p => p.Office > minDate && p.Office != null);
                _logger.LogInformation($"Nombre d'office dans la base réf avec office > minDate : {refOffices.Count()}");
                int counter = 0;

                foreach (var office in refOffices)
                {
                    try
                    {
                        Lot lot = _fabContext.Lot.FirstOrDefault(p => p.FkOffices == office.PkOffices);
                        if (lot == null)
                        {
                            lot = new Lot();
                            lot.FkParamDetailProc = (int)ProcessusEnum.Nouveaute;
                            lot.EnCours = false;
                            lot.FkOffices = office.PkOffices;
                            lot.DateMiseEnVente = office.MiseEnVente;
                            lot.CodeLot = _dateFormater.FormateDateToCodeLot(office.Office.Value, monthsNames);
                            lot.NomLot = _dateFormater.FormatDateToNomLot(office.Office.Value);
                            _fabContext.Add(lot);
                            _fabContext.SaveChanges();
                            counter++;
                            //throw new Exception("test erreur sur un lot");
                        }
                    }
                    catch (Exception ex)
                    {
                        errors.Add($"Erreur lors de la sauvegarde du lot [{office?.Nom}] dans LMG_FAB : {ex.ToString()}");
                        continue;
                    }
                }

                _logger.LogInformation($"Nombre d'office intégrés dans la base LMG_FAB.Lot : {counter.ToString()}");
            }
            catch (Exception ex)
            {
                errors.Add($"Erreur lors de la sauvegarde des lots dans LMG_FAB : {ex.ToString()}");
            }

            return (errors.Count == 0, errors);
        }
    }
}
