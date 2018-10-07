using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LMG.Fab.Data.Entities.LmgFab
{
    public partial class LMG_FABContext : DbContext
    {
        public virtual DbSet<Amalgame> Amalgame { get; set; }
        public virtual DbSet<AmalgameDetail> AmalgameDetail { get; set; }
        public virtual DbSet<Article> Article { get; set; }
        public virtual DbSet<Audit> Audit { get; set; }
        public virtual DbSet<AuditDetail> AuditDetail { get; set; }
        public virtual DbSet<CollectionTechnique> CollectionTechnique { get; set; }
        public virtual DbSet<Commande> Commande { get; set; }
        public virtual DbSet<CommandeLivraison> CommandeLivraison { get; set; }
        public virtual DbSet<Commentaire> Commentaire { get; set; }
        public virtual DbSet<Composant> Composant { get; set; }
        public virtual DbSet<DateJalon> DateJalon { get; set; }
        public virtual DbSet<Devis> Devis { get; set; }
        public virtual DbSet<Dossier> Dossier { get; set; }
        public virtual DbSet<ElAccessoire> ElAccessoire { get; set; }
        public virtual DbSet<Element> Element { get; set; }
        public virtual DbSet<ElementFichePapier> ElementFichePapier { get; set; }
        public virtual DbSet<ElFaconnage> ElFaconnage { get; set; }
        public virtual DbSet<ElImpositionCahier> ElImpositionCahier { get; set; }
        public virtual DbSet<ElImpositionFeuille> ElImpositionFeuille { get; set; }
        public virtual DbSet<ElImpression> ElImpression { get; set; }
        public virtual DbSet<ElPapier> ElPapier { get; set; }
        public virtual DbSet<FichePapier> FichePapier { get; set; }
        public virtual DbSet<Lot> Lot { get; set; }
        public virtual DbSet<Machine> Machine { get; set; }
        public virtual DbSet<MachineGamme> MachineGamme { get; set; }
        public virtual DbSet<ParamDetail> ParamDetail { get; set; }
        public virtual DbSet<ParamTable> ParamTable { get; set; }
        public virtual DbSet<Prestation> Prestation { get; set; }
        public virtual DbSet<Produit> Produit { get; set; }
        public virtual DbSet<ProduitGamme> ProduitGamme { get; set; }
        public virtual DbSet<Tiers> Tiers { get; set; }
        public virtual DbSet<TypeElement> TypeElement { get; set; }
        public virtual DbSet<TypeElementLien> TypeElementLien { get; set; }
        public virtual DbSet<TypeElementProcessus> TypeElementProcessus { get; set; }
        public virtual DbSet<TypeJalon> TypeJalon { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VwLot>(entity =>
            {
                entity.HasKey(e => e.PkLot);
                entity.ToTable("vw_lot");
                entity.Property(e => e.PkLot).HasColumnName("pk_lot");
                entity.Property(e => e.FkOffices).HasColumnName("fk_offices");
                entity.Property(e => e.FkParamDetailProc).HasColumnName("fk_paramDetail_Proc");
            });

            modelBuilder.Entity<Amalgame>(entity =>
            {
                entity.HasKey(e => e.PkAmalgame);

                entity.ToTable("amalgame");

                entity.Property(e => e.PkAmalgame).HasColumnName("pk_amalgame");

                entity.Property(e => e.FkLot).HasColumnName("fk_lot");

                entity.Property(e => e.FkTiers).HasColumnName("fk_tiers");

                entity.Property(e => e.FkTypeElement).HasColumnName("fk_typeElement");

                entity.Property(e => e.Nom)
                    .HasColumnName("nom")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AmalgameDetail>(entity =>
            {
                entity.HasKey(e => e.PkAmalgameDetail);

                entity.ToTable("amalgameDetail");

                entity.Property(e => e.PkAmalgameDetail).HasColumnName("pk_amalgameDetail");

                entity.Property(e => e.FkAmalgame).HasColumnName("fk_amalgame");

                entity.Property(e => e.FkElement).HasColumnName("fk_element");
            });

            modelBuilder.Entity<Article>(entity =>
            {
                entity.HasKey(e => e.PkArticle);

                entity.ToTable("article");

                entity.Property(e => e.PkArticle).HasColumnName("pk_article");

                entity.Property(e => e.FkParamDetailEdit)
                    .HasColumnName("fk_paramDetail_edit")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.FkParamDetailForm).HasColumnName("fk_paramDetail_form");

                entity.Property(e => e.FkParamDetailGamm).HasColumnName("fk_paramDetail_gamm");

                entity.Property(e => e.FkParamDetailTyac).HasColumnName("fk_paramDetail_tyac");

                entity.Property(e => e.Hauteur)
                    .HasColumnName("hauteur")
                    .HasColumnType("numeric(7, 2)");

                entity.Property(e => e.IdIntranet).HasColumnName("idIntranet");

                entity.Property(e => e.Largeur)
                    .HasColumnName("largeur")
                    .HasColumnType("numeric(7, 2)");

                entity.Property(e => e.MotsCles)
                    .HasColumnName("motsCles")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NbPages).HasColumnName("nbPages");

                entity.Property(e => e.NbPagesHorsTexte).HasColumnName("nbPagesHorsTexte");

                entity.Property(e => e.NbPagesHorsTexteDef).HasColumnName("nbPagesHorsTexteDef");

                entity.Property(e => e.NbPagesInterieurDef).HasColumnName("nbPagesInterieurDef");

                entity.Property(e => e.NbSignesCalibres).HasColumnName("nbSignesCalibres");

                entity.Property(e => e.NbSignesEvalues).HasColumnName("nbSignesEvalues");

                entity.Property(e => e.Observation)
                    .HasColumnName("observation")
                    .IsUnicode(false);

                entity.Property(e => e.ParCombien).HasColumnName("parCombien");

                entity.Property(e => e.Titre)
                    .HasColumnName("titre")
                    .HasMaxLength(80)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Audit>(entity =>
            {
                entity.Property(e => e.OperationDate).HasColumnType("datetime");

                entity.Property(e => e.OperationType)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.RecordName).HasMaxLength(256);

                entity.Property(e => e.TableName)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<AuditDetail>(entity =>
            {
                entity.ToTable("Audit_Detail");

                entity.Property(e => e.Field)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.Audit)
                    .WithMany(p => p.AuditDetail)
                    .HasForeignKey(d => d.AuditId)
                    .HasConstraintName("FK_AuditId");
            });

            modelBuilder.Entity<CollectionTechnique>(entity =>
            {
                entity.HasKey(e => e.PkCollectionTechnique);

                entity.ToTable("collectionTechnique");

                entity.Property(e => e.PkCollectionTechnique).HasColumnName("pk_collectionTechnique");

                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FkParamDetailGamm).HasColumnName("fk_paramDetail_gamm");

                entity.Property(e => e.FkParamDetailPellCouv).HasColumnName("fk_paramDetail_pell_couv");

                entity.Property(e => e.FkParamDetailPellCouvr).HasColumnName("fk_paramDetail_pell_couvr");

                entity.Property(e => e.FkParamDetailPellJaquette).HasColumnName("fk_paramDetail_pell_jaquette");

                entity.Property(e => e.FkParamDetailVernCouv).HasColumnName("fk_paramDetail_vern_couv");

                entity.Property(e => e.FkParamDetailVernCouvr).HasColumnName("fk_paramDetail_vern_couvr");

                entity.Property(e => e.FkParamDetailVernJaquette).HasColumnName("fk_paramDetail_vern_jaquette");

                entity.Property(e => e.NbJustificatifs).HasColumnName("nbJustificatifs");

                entity.Property(e => e.Nom)
                    .HasColumnName("nom")
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.OkActif)
                    .HasColumnName("okActif")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.TexteJustificatif)
                    .HasColumnName("texteJustificatif")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Tri).HasColumnName("tri");
            });

            modelBuilder.Entity<Commande>(entity =>
            {
                entity.HasKey(e => e.PkCommande);

                entity.ToTable("commande");

                entity.Property(e => e.PkCommande).HasColumnName("pk_commande");

                entity.Property(e => e.FkDevis).HasColumnName("fk_devis");

                entity.Property(e => e.FkTiersFournisseur).HasColumnName("fk_tiers_fournisseur");

                entity.Property(e => e.NumCommandeErp)
                    .HasColumnName("numCommandeErp")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CommandeLivraison>(entity =>
            {
                entity.HasKey(e => e.PkCommandeLivraison);

                entity.ToTable("commandeLivraison");

                entity.Property(e => e.PkCommandeLivraison).HasColumnName("pk_commandeLivraison");

                entity.Property(e => e.Adresse1)
                    .HasColumnName("adresse1")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Adresse2)
                    .HasColumnName("adresse2")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Adresse3)
                    .HasColumnName("adresse3")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.CodePostal)
                    .HasColumnName("codePostal")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DateLivraison)
                    .HasColumnName("dateLivraison")
                    .HasColumnType("date");

                entity.Property(e => e.FkCommande).HasColumnName("fk_commande");

                entity.Property(e => e.FkTiersLivraison).HasColumnName("fk_tiers_livraison");

                entity.Property(e => e.Nom)
                    .HasColumnName("nom")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Pays)
                    .HasColumnName("pays")
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.Quantite).HasColumnName("quantite");

                entity.Property(e => e.Ville)
                    .HasColumnName("ville")
                    .HasMaxLength(35)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Commentaire>(entity =>
            {
                entity.HasKey(e => e.PkCommentaire);

                entity.ToTable("commentaire");

                entity.Property(e => e.PkCommentaire).HasColumnName("pk_commentaire");

                entity.Property(e => e.CommentaireHtml).HasColumnName("commentaireHtml");

                entity.Property(e => e.FieldComm)
                    .HasColumnName("fieldComm")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.IsImportant).HasColumnName("isImportant");

                entity.Property(e => e.LastUpdate)
                    .HasColumnName("lastUpdate")
                    .HasColumnType("datetime");

                entity.Property(e => e.RecordKey).HasColumnName("recordKey");

                entity.Property(e => e.RecordName)
                    .HasColumnName("recordName")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TableComm)
                    .IsRequired()
                    .HasColumnName("tableComm")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasColumnName("userName")
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<Composant>(entity =>
            {
                entity.HasKey(e => e.PkComposant);

                entity.ToTable("composant");

                entity.Property(e => e.PkComposant).HasColumnName("pk_composant");

                entity.Property(e => e.FkDossier).HasColumnName("fk_dossier");

                entity.Property(e => e.Nom)
                    .HasColumnName("nom")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DateJalon>(entity =>
            {
                entity.HasKey(e => e.PkDateJalon);

                entity.ToTable("dateJalon");

                entity.Property(e => e.PkDateJalon).HasColumnName("pk_dateJalon");

                entity.Property(e => e.DateJalon1)
                    .HasColumnName("dateJalon")
                    .HasColumnType("date");

                entity.Property(e => e.FkDossier).HasColumnName("fk_dossier");

                entity.Property(e => e.FkTypeJalon).HasColumnName("fk_typeJalon");
            });

            modelBuilder.Entity<Devis>(entity =>
            {
                entity.HasKey(e => e.PkDevis);

                entity.ToTable("devis");

                entity.Property(e => e.PkDevis).HasColumnName("pk_devis");

                entity.Property(e => e.FichierFourni).HasColumnName("fichierFourni");

                entity.Property(e => e.FkCollectionTechnique).HasColumnName("fk_collectionTechnique");

                entity.Property(e => e.FkComposant).HasColumnName("fk_composant");

                entity.Property(e => e.FkParamDetailPres).HasColumnName("fk_paramDetail_pres");

                entity.Property(e => e.FkParamDetailSadm).HasColumnName("fk_paramDetail_sadm");

                entity.Property(e => e.FkParamDetailStfb).HasColumnName("fk_paramDetail_stfb");

                entity.Property(e => e.NbHeuresSuiviFab).HasColumnName("nbHeuresSuiviFab");

                entity.Property(e => e.OkActif).HasColumnName("okActif");
            });

            modelBuilder.Entity<Dossier>(entity =>
            {
                entity.HasKey(e => e.PkDossier);

                entity.ToTable("dossier");

                entity.Property(e => e.PkDossier).HasColumnName("pk_dossier");

                entity.Property(e => e.FausseFoliotation).HasColumnName("fausseFoliotation");

                entity.Property(e => e.FilierePrincipale)
                    .HasColumnName("filierePrincipale")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FkLot).HasColumnName("fk_lot");

                entity.Property(e => e.FkParamDetailEqfb).HasColumnName("fk_paramDetail_eqfb");

                entity.Property(e => e.FkParamDetailProc).HasColumnName("fk_paramDetail_proc");

                entity.Property(e => e.IdTirage)
                    .HasColumnName("idTirage")
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.IdTirage4d).HasColumnName("idTirage4d");

                entity.Property(e => e.OkCoffret).HasColumnName("okCoffret");

                entity.Property(e => e.OkEligibleTechniqueEpac).HasColumnName("okEligibleTechniqueEpac");

                entity.Property(e => e.OkLivraisonAnticipee).HasColumnName("okLivraisonAnticipee");

                entity.Property(e => e.OkQteTirageDef).HasColumnName("okQteTirageDef");

                entity.Property(e => e.PrixTtc)
                    .HasColumnName("prixTTC")
                    .HasColumnType("numeric(9, 2)");

                entity.Property(e => e.QteTirage).HasColumnName("qteTirage");

                entity.Property(e => e.TxtModeleComposition)
                    .HasColumnName("txtModeleComposition")
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ElAccessoire>(entity =>
            {
                entity.HasKey(e => e.PkElement);

                entity.ToTable("elAccessoire");

                entity.Property(e => e.PkElement)
                    .HasColumnName("pk_element")
                    .ValueGeneratedNever();

                entity.Property(e => e.QteTirageReimp).HasColumnName("qteTirageReimp");
            });

            modelBuilder.Entity<Element>(entity =>
            {
                entity.HasKey(e => e.PkElement);

                entity.ToTable("element");

                entity.Property(e => e.PkElement).HasColumnName("pk_element");

                entity.Property(e => e.FkDevis).HasColumnName("fk_devis");

                entity.Property(e => e.FkElementParent).HasColumnName("fk_element_parent");

                entity.Property(e => e.FkTiersFournisseur).HasColumnName("fk_tiers_fournisseur");

                entity.Property(e => e.FkTypeElement).HasColumnName("fk_typeElement");

                entity.Property(e => e.FraisFixe)
                    .HasColumnName("fraisFixe")
                    .HasColumnType("numeric(9, 2)");

                entity.Property(e => e.FraisVariable)
                    .HasColumnName("fraisVariable")
                    .HasColumnType("numeric(9, 2)");

                entity.Property(e => e.MontantCalculCommande)
                    .HasColumnName("montantCalculCommande")
                    .HasColumnType("numeric(9, 2)");

                entity.Property(e => e.MontantCalculDevis)
                    .HasColumnName("montantCalculDevis")
                    .HasColumnType("numeric(9, 2)");

                entity.Property(e => e.OkCommande).HasColumnName("okCommande");

                entity.Property(e => e.PortFixe)
                    .HasColumnName("portFixe")
                    .HasColumnType("numeric(9, 2)");
            });

            modelBuilder.Entity<ElementFichePapier>(entity =>
            {
                entity.HasKey(e => e.PkElementFichePapier);

                entity.ToTable("elementFichePapier");

                entity.Property(e => e.PkElementFichePapier).HasColumnName("pk_elementFichePapier");

                entity.Property(e => e.FkElement).HasColumnName("fk_element");

                entity.Property(e => e.FkFichePapier).HasColumnName("fk_fichePapier");
            });

            modelBuilder.Entity<ElFaconnage>(entity =>
            {
                entity.HasKey(e => e.PkElement);

                entity.ToTable("elFaconnage");

                entity.Property(e => e.PkElement)
                    .HasColumnName("pk_element")
                    .ValueGeneratedNever();

                entity.Property(e => e.FaconnerTout).HasColumnName("faconnerTout");

                entity.Property(e => e.FerAdorer)
                    .HasColumnName("ferADorer")
                    .HasColumnType("numeric(9, 2)");

                entity.Property(e => e.FkParamDetailGard).HasColumnName("fk_paramDetail_gard");

                entity.Property(e => e.FkParamDetailTbro).HasColumnName("fk_paramDetail_tbro");

                entity.Property(e => e.FkProduitToile).HasColumnName("fk_produit_toile");

                entity.Property(e => e.FraisFixePose)
                    .HasColumnName("fraisFixePose")
                    .HasColumnType("numeric(9, 2)");

                entity.Property(e => e.FraisVariablePose)
                    .HasColumnName("fraisVariablePose")
                    .HasColumnType("numeric(9, 2)");

                entity.Property(e => e.Quantite).HasColumnName("quantite");

                entity.Property(e => e.ValeurToile)
                    .HasColumnName("valeurToile")
                    .HasColumnType("numeric(9, 2)");
            });

            modelBuilder.Entity<ElImpositionCahier>(entity =>
            {
                entity.HasKey(e => e.PkElement);

                entity.ToTable("elImpositionCahier");

                entity.Property(e => e.PkElement)
                    .HasColumnName("pk_element")
                    .ValueGeneratedNever();

                entity.Property(e => e.NbCahiers1).HasColumnName("nbCahiers1");

                entity.Property(e => e.NbCahiers2).HasColumnName("nbCahiers2");

                entity.Property(e => e.NbCahiers3).HasColumnName("nbCahiers3");

                entity.Property(e => e.NbCahiers4).HasColumnName("nbCahiers4");

                entity.Property(e => e.NbPagesParCahier1).HasColumnName("nbPagesParCahier1");

                entity.Property(e => e.NbPagesParCahier2).HasColumnName("nbPagesParCahier2");

                entity.Property(e => e.NbPagesParCahier3).HasColumnName("nbPagesParCahier3");

                entity.Property(e => e.NbPagesParCahier4).HasColumnName("nbPagesParCahier4");
            });

            modelBuilder.Entity<ElImpositionFeuille>(entity =>
            {
                entity.HasKey(e => e.PkElement);

                entity.ToTable("elImpositionFeuille");

                entity.Property(e => e.PkElement)
                    .HasColumnName("pk_element")
                    .ValueGeneratedNever();

                entity.Property(e => e.Imposition64Pages).HasColumnName("imposition64Pages");

                entity.Property(e => e.NbFeuilles1)
                    .HasColumnName("nbFeuilles1")
                    .HasColumnType("numeric(9, 2)");

                entity.Property(e => e.NbFeuilles2)
                    .HasColumnName("nbFeuilles2")
                    .HasColumnType("numeric(9, 2)");

                entity.Property(e => e.NbFeuilles3)
                    .HasColumnName("nbFeuilles3")
                    .HasColumnType("numeric(9, 2)");

                entity.Property(e => e.NbPagesChute).HasColumnName("nbPagesChute");

                entity.Property(e => e.NbPagesParFeuille1).HasColumnName("nbPagesParFeuille1");

                entity.Property(e => e.NbPagesParFeuille2).HasColumnName("nbPagesParFeuille2");

                entity.Property(e => e.NbPagesParFeuille3).HasColumnName("nbPagesParFeuille3");
            });

            modelBuilder.Entity<ElImpression>(entity =>
            {
                entity.HasKey(e => e.PkElImpression);

                entity.ToTable("elImpression");

                entity.Property(e => e.PkElImpression)
                    .HasColumnName("pk_elImpression")
                    .ValueGeneratedNever();

                entity.Property(e => e.CoutFilm)
                    .HasColumnName("coutFilm")
                    .HasColumnType("numeric(9, 2)");

                entity.Property(e => e.EquerrageFraisFixe)
                    .HasColumnName("equerrageFraisFixe")
                    .HasColumnType("numeric(9, 2)");

                entity.Property(e => e.EquerrageFraisVariable)
                    .HasColumnName("equerrageFraisVariable")
                    .HasColumnType("numeric(9, 2)");

                entity.Property(e => e.FkMachine).HasColumnName("fk_machine");

                entity.Property(e => e.FkParamDetailCoulRecto).HasColumnName("fk_paramDetail_coul_recto");

                entity.Property(e => e.FkParamDetailCoulVerso).HasColumnName("fk_paramDetail_coul_verso");

                entity.Property(e => e.FkParamDetailFilm).HasColumnName("fk_paramDetail_film");

                entity.Property(e => e.FkParamDetailPell).HasColumnName("fk_paramDetail_pell");

                entity.Property(e => e.FkParamDetailVern).HasColumnName("fk_paramDetail_vern");

                entity.Property(e => e.FkParamdetailType).HasColumnName("fk_paramdetail_type");

                entity.Property(e => e.OkCoteAcote).HasColumnName("okCoteACote");

                entity.Property(e => e.OkEquerrage).HasColumnName("okEquerrage");

                entity.Property(e => e.OkSupplementSimili).HasColumnName("okSupplementSimili");
            });

            modelBuilder.Entity<ElPapier>(entity =>
            {
                entity.HasKey(e => e.PkElement);

                entity.ToTable("elPapier");

                entity.Property(e => e.PkElement)
                    .HasColumnName("pk_element")
                    .ValueGeneratedNever();

                entity.Property(e => e.FkParamDetailQual).HasColumnName("fk_paramDetail_qual");

                entity.Property(e => e.FkProduit).HasColumnName("fk_produit");

                entity.Property(e => e.Grammage)
                    .HasColumnName("grammage")
                    .HasColumnType("numeric(9, 2)");

                entity.Property(e => e.HauteurMm)
                    .HasColumnName("hauteurMm")
                    .HasColumnType("numeric(9, 2)");

                entity.Property(e => e.LargeurMm)
                    .HasColumnName("largeurMm")
                    .HasColumnType("numeric(9, 2)");

                entity.Property(e => e.Main)
                    .HasColumnName("main")
                    .HasColumnType("numeric(9, 2)");

                entity.Property(e => e.PapierEco).HasColumnName("papierEco");

                entity.Property(e => e.Prix)
                    .HasColumnName("prix")
                    .HasColumnType("numeric(9, 2)");
            });

            modelBuilder.Entity<FichePapier>(entity =>
            {
                entity.HasKey(e => e.PkFichePapier);

                entity.ToTable("fichePapier");

                entity.Property(e => e.PkFichePapier).HasColumnName("pk_fichePapier");

                entity.Property(e => e.Denomination)
                    .HasColumnName("denomination")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Grammage).HasColumnName("grammage");

                entity.Property(e => e.Hauteur)
                    .HasColumnName("hauteur")
                    .HasColumnType("numeric(9, 2)");

                entity.Property(e => e.LargeurMm)
                    .HasColumnName("largeurMm")
                    .HasColumnType("numeric(9, 2)");

                entity.Property(e => e.NumFiche).HasColumnName("numFiche");
            });

            modelBuilder.Entity<Lot>(entity =>
            {
                entity.HasKey(e => e.PkLot);

                entity.ToTable("lot");

                entity.Property(e => e.PkLot).HasColumnName("pk_lot");

                entity.Property(e => e.CodeLot)
                    .IsRequired()
                    .HasColumnName("codeLot")
                    .HasColumnType("nchar(10)");

                entity.Property(e => e.DateArriveeLivres)
                    .HasColumnName("dateArriveeLivres")
                    .HasColumnType("date");

                entity.Property(e => e.DateMiseEnVente)
                    .HasColumnName("dateMiseEnVente")
                    .HasColumnType("date");

                entity.Property(e => e.EnCours).HasColumnName("enCours");

                entity.Property(e => e.FkOffices).HasColumnName("fk_offices");

                entity.Property(e => e.FkParamDetailProc).HasColumnName("fk_paramDetail_Proc");

                entity.Property(e => e.NomLot)
                    .IsRequired()
                    .HasColumnName("nomLot")
                    .HasMaxLength(20);

                entity.HasOne(d => d.FkParamDetailProcNavigation)
                    .WithMany(p => p.Lot)
                    .HasForeignKey(d => d.FkParamDetailProc)
                    .HasConstraintName("FK_lot_processus");
            });

            modelBuilder.Entity<Machine>(entity =>
            {
                entity.HasKey(e => e.PkMachine);

                entity.ToTable("machine");

                entity.Property(e => e.PkMachine).HasColumnName("pk_machine");

                entity.Property(e => e.Cameron).HasColumnName("cameron");

                entity.Property(e => e.Coming).HasColumnName("coming");

                entity.Property(e => e.FausseCoupe)
                    .HasColumnName("fausseCoupe")
                    .HasColumnType("numeric(9, 2)");

                entity.Property(e => e.FkPrestation).HasColumnName("fk_prestation");

                entity.Property(e => e.Nom)
                    .HasColumnName("nom")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.OkNumerique).HasColumnName("okNumerique");

                entity.Property(e => e.Roto).HasColumnName("roto");

                entity.Property(e => e.SousMultiplesCahiers)
                    .HasColumnName("sousMultiplesCahiers")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Tournees)
                    .HasColumnName("tournees")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkPrestationNavigation)
                    .WithMany(p => p.Machine)
                    .HasForeignKey(d => d.FkPrestation)
                    .HasConstraintName("FK_machine_prestation");
            });

            modelBuilder.Entity<MachineGamme>(entity =>
            {
                entity.HasKey(e => e.PkMachineGamme);

                entity.ToTable("machineGamme");

                entity.Property(e => e.PkMachineGamme).HasColumnName("pk_machineGamme");

                entity.Property(e => e.Actif)
                    .HasColumnName("actif")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CodeDatexp)
                    .HasColumnName("codeDatexp")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FkMachine).HasColumnName("fk_machine");

                entity.Property(e => e.FkParamDetailGamm).HasColumnName("fk_paramDetail_gamm");

                entity.HasOne(d => d.FkMachineNavigation)
                    .WithMany(p => p.MachineGamme)
                    .HasForeignKey(d => d.FkMachine)
                    .HasConstraintName("FK_machineGamme_machine");

                entity.HasOne(d => d.FkParamDetailGammNavigation)
                    .WithMany(p => p.MachineGamme)
                    .HasForeignKey(d => d.FkParamDetailGamm)
                    .HasConstraintName("FK_machineGamme_paramDetail");
            });

            modelBuilder.Entity<ParamDetail>(entity =>
            {
                entity.HasKey(e => e.PkParamDetail);

                entity.ToTable("paramDetail");

                entity.Property(e => e.PkParamDetail).HasColumnName("pk_paramDetail");

                entity.Property(e => e.Actif)
                    .HasColumnName("actif")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FkParamTable).HasColumnName("fk_paramTable");

                entity.Property(e => e.LibelleCourt)
                    .HasColumnName("libelleCourt")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LibelleLong)
                    .HasColumnName("libelleLong")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Tri).HasColumnName("tri");

                entity.HasOne(d => d.FkParamTableNavigation)
                    .WithMany(p => p.ParamDetail)
                    .HasForeignKey(d => d.FkParamTable)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_paramTable_paramDetail");
            });

            modelBuilder.Entity<ParamTable>(entity =>
            {
                entity.HasKey(e => e.PkParamTable);

                entity.ToTable("paramTable");

                entity.Property(e => e.PkParamTable).HasColumnName("pk_paramTable");

                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CodeDefaut)
                    .HasColumnName("codeDefaut")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LibelleCourt)
                    .HasColumnName("libelleCourt")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LibelleLong)
                    .HasColumnName("libelleLong")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Prestation>(entity =>
            {
                entity.HasKey(e => e.PkPrestation);

                entity.ToTable("prestation");

                entity.Property(e => e.PkPrestation).HasColumnName("pk_prestation");

                entity.Property(e => e.FkParamDetailTrfo).HasColumnName("fk_paramDetail_trfo");

                entity.Property(e => e.FkTiers).HasColumnName("fk_tiers");

                entity.Property(e => e.TauxHoraire)
                    .HasColumnName("tauxHoraire")
                    .HasColumnType("numeric(9, 2)");

                entity.HasOne(d => d.FkParamDetailTrfoNavigation)
                    .WithMany(p => p.Prestation)
                    .HasForeignKey(d => d.FkParamDetailTrfo)
                    .HasConstraintName("FK_prestation_paramDetail");

                entity.HasOne(d => d.FkTiersNavigation)
                    .WithMany(p => p.Prestation)
                    .HasForeignKey(d => d.FkTiers)
                    .HasConstraintName("FK_prestation_tiers");
            });

            modelBuilder.Entity<Produit>(entity =>
            {
                entity.HasKey(e => e.PkProduit);

                entity.ToTable("produit");

                entity.Property(e => e.PkProduit).HasColumnName("pk_produit");

                entity.Property(e => e.DefiniParUtil).HasColumnName("definiParUtil");

                entity.Property(e => e.Famille)
                    .HasColumnName("famille")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FkParamDetailQual).HasColumnName("fk_paramDetail_qual");

                entity.Property(e => e.Grammage)
                    .HasColumnName("grammage")
                    .HasColumnType("numeric(9, 2)");

                entity.Property(e => e.HauteurMm)
                    .HasColumnName("hauteurMm")
                    .HasColumnType("numeric(9, 2)");

                entity.Property(e => e.Intitule)
                    .HasColumnName("intitule")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.LargeurMm)
                    .HasColumnName("largeurMm")
                    .HasColumnType("numeric(9, 2)");

                entity.Property(e => e.Main)
                    .HasColumnName("main")
                    .HasColumnType("numeric(9, 2)");

                entity.Property(e => e.OkFeuille).HasColumnName("okFeuille");

                entity.Property(e => e.OkRoto).HasColumnName("okRoto");

                entity.Property(e => e.PrixKg)
                    .HasColumnName("prixKg")
                    .HasColumnType("numeric(9, 3)");
            });

            modelBuilder.Entity<ProduitGamme>(entity =>
            {
                entity.HasKey(e => e.PkProduitGamme);

                entity.ToTable("produitGamme");

                entity.Property(e => e.PkProduitGamme).HasColumnName("pk_produitGamme");

                entity.Property(e => e.FkParamDetailGamm).HasColumnName("fk_paramDetail_gamm");

                entity.Property(e => e.FkProduit).HasColumnName("fk_produit");
            });

            modelBuilder.Entity<Tiers>(entity =>
            {
                entity.HasKey(e => e.PkTiers);

                entity.ToTable("tiers");

                entity.Property(e => e.PkTiers).HasColumnName("pk_tiers");

                entity.Property(e => e.Actif).HasColumnName("actif");

                entity.Property(e => e.Adresse1)
                    .HasColumnName("adresse1")
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.Adresse2)
                    .HasColumnName("adresse2")
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.Adresse3)
                    .HasColumnName("adresse3")
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.AdresseEmail)
                    .HasColumnName("adresseEmail")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CodeEntrepotErp)
                    .HasColumnName("codeEntrepotERP")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CodeFournisseurErp)
                    .HasColumnName("codeFournisseurERP")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CodePostal)
                    .HasColumnName("codePostal")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FkParamDetailType).HasColumnName("fk_paramDetail_type");

                entity.Property(e => e.Nom)
                    .HasColumnName("nom")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.NomContact)
                    .HasColumnName("nomContact")
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.OkImprimvert).HasColumnName("okImprimvert");

                entity.Property(e => e.Pays)
                    .HasColumnName("pays")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.SignatureFicheTechnique)
                    .HasColumnName("signatureFicheTechnique")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Telephone)
                    .HasColumnName("telephone")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TexteCommande)
                    .HasColumnName("texteCommande")
                    .IsUnicode(false);

                entity.Property(e => e.Ville)
                    .HasColumnName("ville")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkParamDetailTypeNavigation)
                    .WithMany(p => p.Tiers)
                    .HasForeignKey(d => d.FkParamDetailType)
                    .HasConstraintName("FK_tiers_paramDetail");
            });

            modelBuilder.Entity<TypeElement>(entity =>
            {
                entity.HasKey(e => e.PkTypeElement);

                entity.ToTable("typeElement");

                entity.Property(e => e.PkTypeElement).HasColumnName("pk_typeElement");

                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Nom)
                    .HasColumnName("nom")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OkComposition).HasColumnName("okComposition");

                entity.Property(e => e.OkFaconnage).HasColumnName("okFaconnage");

                entity.Property(e => e.OkImpositionCahier).HasColumnName("okImpositionCahier");

                entity.Property(e => e.OkImpositionFeuille).HasColumnName("okImpositionFeuille");

                entity.Property(e => e.OkImpression).HasColumnName("okImpression");

                entity.Property(e => e.OkMultiple).HasColumnName("okMultiple");

                entity.Property(e => e.OkObligatoire).HasColumnName("okObligatoire");

                entity.Property(e => e.OkPapier).HasColumnName("okPapier");

                entity.Property(e => e.OkPrincipal).HasColumnName("okPrincipal");

                entity.Property(e => e.Tri).HasColumnName("tri");
            });

            modelBuilder.Entity<TypeElementLien>(entity =>
            {
                entity.HasKey(e => e.PkTypeElementLien);

                entity.ToTable("typeElementLien");

                entity.Property(e => e.PkTypeElementLien).HasColumnName("pk_typeElementLien");

                entity.Property(e => e.FkTypeElementEnfant).HasColumnName("fk_typeElement_enfant");

                entity.Property(e => e.FkTypeElementParent).HasColumnName("fk_typeElement_parent");

                entity.Property(e => e.OkHeritageFournisseur).HasColumnName("okHeritageFournisseur");
            });

            modelBuilder.Entity<TypeElementProcessus>(entity =>
            {
                entity.HasKey(e => e.PkTypeElementProcessus);

                entity.ToTable("typeElementProcessus");

                entity.Property(e => e.PkTypeElementProcessus).HasColumnName("pk_typeElementProcessus");

                entity.Property(e => e.FkParamDetailProc).HasColumnName("fk_paramDetail_proc");

                entity.Property(e => e.FkTypeElement).HasColumnName("fk_typeElement");
            });

            modelBuilder.Entity<TypeJalon>(entity =>
            {
                entity.HasKey(e => e.PkTypeJalon);

                entity.ToTable("typeJalon");

                entity.Property(e => e.PkTypeJalon).HasColumnName("pk_typeJalon");

                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Nom)
                    .HasColumnName("nom")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Tri).HasColumnName("tri");
            });
        }
    }
}
