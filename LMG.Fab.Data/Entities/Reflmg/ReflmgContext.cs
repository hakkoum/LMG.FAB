using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LMG.Fab.Data.Entities.Reflmg
{
    public partial class ReflmgContext : DbContext
    {
        public virtual DbSet<Application> Application { get; set; }
        public virtual DbSet<Contributeur> Contributeur { get; set; }
        public virtual DbSet<Fonction> Fonction { get; set; }
        public virtual DbSet<NomPerimetre> NomPerimetre { get; set; }
        public virtual DbSet<Offices> Offices { get; set; }
        public virtual DbSet<Perimetre> Perimetre { get; set; }
        public virtual DbSet<Profile> Profile { get; set; }
        public virtual DbSet<ProfileFonction> ProfileFonction { get; set; }
        public virtual DbSet<RoleParApplication> RoleParApplication { get; set; }

        public ReflmgContext(DbContextOptions<ReflmgContext> options) : base(options) { }

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //                optionsBuilder.UseSqlServer(@"Data Source=lmgvmproj2k8\rec;Initial Catalog=Reflmg;Integrated Security=True");
        //            }
        //        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Application>(entity =>
            {
                entity.HasKey(e => e.PkApplication);

                entity.ToTable("application");

                entity.HasIndex(e => e.Code)
                    .HasName("UK_Code")
                    .IsUnique();

                entity.Property(e => e.PkApplication).HasColumnName("pk_application");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnName("code")
                    .HasMaxLength(10);

                entity.Property(e => e.Icone)
                    .HasColumnName("icone")
                    .HasMaxLength(255);

                entity.Property(e => e.Nom)
                    .IsRequired()
                    .HasColumnName("nom")
                    .HasMaxLength(255);

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasColumnName("url")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Contributeur>(entity =>
            {
                entity.HasKey(e => e.PkContributeur);

                entity.ToTable("contributeur");

                entity.HasIndex(e => e.Login)
                    .HasName("idx_contributeur_login");

                entity.Property(e => e.PkContributeur).HasColumnName("pk_contributeur");

                entity.Property(e => e.CortexCulture)
                    .HasColumnName("cortexCulture")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50);

                entity.Property(e => e.Gad)
                    .HasColumnName("GAD")
                    .HasMaxLength(65);

                entity.Property(e => e.Guid)
                    .IsRequired()
                    .HasColumnName("GUID")
                    .HasMaxLength(36);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnName("login")
                    .HasMaxLength(80);

                entity.Property(e => e.Nom)
                    .HasColumnName("nom")
                    .HasMaxLength(80)
                    .HasDefaultValueSql("('6')");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(128);

                entity.Property(e => e.PrefFormat).HasColumnName("prefFormat");

                entity.Property(e => e.PrefLignes)
                    .HasColumnName("prefLignes")
                    .HasDefaultValueSql("((48))");

                entity.Property(e => e.Prenom)
                    .HasColumnName("prenom")
                    .HasMaxLength(80);

                entity.Property(e => e.ResetToken)
                    .HasColumnName("resetToken")
                    .HasMaxLength(36);
            });

            modelBuilder.Entity<Fonction>(entity =>
            {
                entity.HasKey(e => e.PkFonction);

                entity.ToTable("fonction");

                entity.Property(e => e.PkFonction).HasColumnName("pk_fonction");

                entity.Property(e => e.Application)
                    .HasColumnName("application")
                    .HasMaxLength(255);

                entity.Property(e => e.Code).HasMaxLength(255);

                entity.Property(e => e.CortexCulture)
                    .HasColumnName("cortexCulture")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("ntext");

                entity.Property(e => e.FkReference).HasColumnName("fk_reference");

                entity.Property(e => e.Nom)
                    .HasColumnName("nom")
                    .HasMaxLength(255);

                entity.Property(e => e.PosReference).HasColumnName("pos_reference");

                entity.Property(e => e.Redirection)
                    .HasColumnName("redirection")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<NomPerimetre>(entity =>
            {
                entity.HasKey(e => e.Nomperimetre);

                entity.ToTable("nomPerimetre");

                entity.Property(e => e.Nomperimetre)
                    .HasColumnName("nomperimetre")
                    .HasMaxLength(255)
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<Offices>(entity =>
            {
                entity.HasKey(e => e.PkOffices);

                entity.HasIndex(e => e.Office)
                    .HasName("idx_office_office");

                entity.HasIndex(e => new { e.MiseEnVente, e.Office, e.PkOffices })
                    .HasName("_dta_index_Offices_12_1621580815__K2_K8_6");

                entity.HasIndex(e => new { e.CortexCulture, e.DateCreation, e.DossierBleu, e.Facturation, e.LimiteLivraison, e.MiseEnVente, e.Nom, e.Office, e.PkOffices })
                    .HasName("_dta_index_Offices_12_1621580815__K2_K8_1_3_4_5_6_7_9");

                entity.Property(e => e.PkOffices).HasColumnName("pk_Offices");

                entity.Property(e => e.CodeOfficeNumItf)
                    .HasColumnName("codeOfficeNumITF")
                    .HasMaxLength(5);

                entity.Property(e => e.CodeOfficePapierItf)
                    .HasColumnName("codeOfficePapierITF")
                    .HasMaxLength(5);

                entity.Property(e => e.CortexCulture)
                    .HasColumnName("cortexCulture")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.DateCreation)
                    .HasColumnName("dateCreation")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DossierBleu)
                    .HasColumnName("dossierBleu")
                    .HasColumnType("datetime");

                entity.Property(e => e.Facturation)
                    .HasColumnName("facturation")
                    .HasColumnType("datetime");

                entity.Property(e => e.LimiteLivraison)
                    .HasColumnName("limiteLivraison")
                    .HasColumnType("datetime");

                entity.Property(e => e.MiseEnVente)
                    .HasColumnName("miseEnVente")
                    .HasColumnType("datetime");

                entity.Property(e => e.Nom)
                    .HasColumnName("nom")
                    .HasMaxLength(80);

                entity.Property(e => e.Office)
                    .HasColumnName("office")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Perimetre>(entity =>
            {
                entity.HasKey(e => e.PkPerimetre);

                entity.ToTable("perimetre");

                entity.Property(e => e.PkPerimetre).HasColumnName("pk_perimetre");

                entity.Property(e => e.ClauseWhere)
                    .HasColumnName("clauseWhere")
                    .HasMaxLength(255);

                entity.Property(e => e.CortexCulture)
                    .HasColumnName("cortexCulture")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Esql)
                    .HasColumnName("esql")
                    .HasMaxLength(255);

                entity.Property(e => e.Filtre)
                    .HasColumnName("filtre")
                    .HasMaxLength(255);

                entity.Property(e => e.LibelleSso)
                    .HasColumnName("libelleSso")
                    .HasMaxLength(100);

                entity.Property(e => e.Nom)
                    .HasColumnName("nom")
                    .HasMaxLength(255);

                entity.Property(e => e.Objet)
                    .HasColumnName("objet")
                    .HasMaxLength(255);

                entity.HasOne(d => d.NomNavigation)
                    .WithMany(p => p.Perimetre)
                    .HasForeignKey(d => d.Nom)
                    .HasConstraintName("FK_perimetre_nomPerimetre");
            });

            modelBuilder.Entity<Profile>(entity =>
            {
                entity.HasKey(e => e.PkProfile);

                entity.ToTable("profile");

                entity.Property(e => e.PkProfile).HasColumnName("pk_profile");

                entity.Property(e => e.CortexCulture)
                    .HasColumnName("cortexCulture")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("ntext");

                entity.Property(e => e.LibelleSso)
                    .HasColumnName("libelleSso")
                    .HasMaxLength(100);

                entity.Property(e => e.Nom)
                    .HasColumnName("nom")
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<ProfileFonction>(entity =>
            {
                entity.HasKey(e => e.PkProfileFonction);

                entity.ToTable("profile_fonction");

                entity.Property(e => e.PkProfileFonction).HasColumnName("pk_profile_fonction");

                entity.Property(e => e.FkFonction).HasColumnName("fk_fonction");

                entity.Property(e => e.FkProfile).HasColumnName("fk_profile");

                entity.Property(e => e.PosProfile).HasColumnName("pos_profile");

                entity.HasOne(d => d.FkFonctionNavigation)
                    .WithMany(p => p.ProfileFonction)
                    .HasForeignKey(d => d.FkFonction)
                    .HasConstraintName("FK_NN2_profile_fonction");

                entity.HasOne(d => d.FkProfileNavigation)
                    .WithMany(p => p.ProfileFonction)
                    .HasForeignKey(d => d.FkProfile)
                    .HasConstraintName("FK_NN_profile_fonction");
            });

            modelBuilder.Entity<RoleParApplication>(entity =>
            {
                entity.HasKey(e => new { e.UtilisateurId, e.ProfilId, e.Perimetre, e.ApplicationId });

                entity.Property(e => e.UtilisateurId).HasColumnName("UtilisateurID");

                entity.Property(e => e.ProfilId).HasColumnName("ProfilID");

                entity.Property(e => e.Perimetre).HasMaxLength(255);

                entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.RoleParApplication)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoleParApplication");

                entity.HasOne(d => d.PerimetreNavigation)
                    .WithMany(p => p.RoleParApplication)
                    .HasForeignKey(d => d.Perimetre)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoleParApplication_nomPerimetre");

                entity.HasOne(d => d.Profil)
                    .WithMany(p => p.RoleParApplication)
                    .HasForeignKey(d => d.ProfilId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoleParApplication_profile");

                entity.HasOne(d => d.Utilisateur)
                    .WithMany(p => p.RoleParApplication)
                    .HasForeignKey(d => d.UtilisateurId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoleParApplication_contributeur");
            });
        }
    }
}
