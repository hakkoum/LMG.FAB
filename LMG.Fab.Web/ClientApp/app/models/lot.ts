export class Lot {
    PkLot: number;
    NomLot: string;
    FkParamDetailProc: number;
    CodeLot: string;
    FkOffices?: number;
    DateArriveeLivres?: Date;
    DateMiseEnVente?: Date;
    NomProcessus: string;
    CodeProcessus: string;
    IsSelected: boolean = false;
    EnCours: boolean
}