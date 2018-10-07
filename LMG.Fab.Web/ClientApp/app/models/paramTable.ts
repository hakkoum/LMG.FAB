export class ParamTable {
    PkParamTable: number;
    Code: string;
    LibelleLong: string;
    LibelleCourt: string;
    CodeDefaut: string;
    ParamDetail: ParamDetail[] = []
}


export class ParamDetail {
    PkParamDetail: number;
    FkParamTable: number;
    Code: string;
    LibelleLong: string;
    LibelleCourt: string;
    Tri: number;
    Actif: Boolean;
}
