import { ParamDetail } from "./paramTable";

export class Tiers {
    public PkTiers: number;
    public FkParamDetailType: number;
    public Nom: string;
    public Adresse1: string;
    public Adresse2: string;
    public Adresse3: string;
    public CodePostal: string;
    public Ville: string;
    public Pays: string;
    public NomContact: string;
    public Telephone: string;
    public CodeFournisseurErp: string;
    public CodeEntrepotErp: string;
    public SignatureFicheTechnique: string;
    public TexteCommande: string;
    public Actif: boolean;
    public AdresseEmail: string;
    public OkImprimvert: boolean;
    public PkPrestation: number;
    public TauxHoraire: number;
    public Prestation: Prestation[];
}

export class Prestation {
    public PkPrestation: number;
    public FkTiers: number;
    public FkParamDetailTrfo: number;
    public TauxHoraire: number;
    public Machine: Machine[];
}

export class Machine {
    public PkMachine: number;
    public Nom: string;
    public Tournees: string;
    public Roto: boolean;
    public Coming: boolean;
    public Cameron: boolean;
    public FausseCoupe: number;
    public FkPrestation: number;
    public SousMultiplesCahiers: string;
    public OkNumerique: boolean;
    public MachineGamme: MachineGamme[];
}

export class MachineGamme {
    public PkMachineGamme: number;
    public FkMachine: number;
    public FkParamDetailGamm: number;
    public CodeDatexp: string;
    public Actif: boolean;
}