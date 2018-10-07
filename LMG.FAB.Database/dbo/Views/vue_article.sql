


CREATE VIEW [dbo].[vue_article]
AS 
SELECT   distinct  livre.pk_livre PkLivre, 
           themeDilicom.CLIL                CLIL,
		   editeur.codeEditeur						codeMaisonEdition,
		   --editeur.nom								nomMaisonEdition,
		   --departementEdito.pk_departementEdito		pk_departementEdito,
		   --departementEdito.codeAx					codeDepartementEdito,	   
		   --departementEdito.nom						nomDepartementEdito,
		   --departementEdito.pos_pole				triDepartementEdito,
		   --collection.numero						codeCollection,
		   collection.nomInterne					nomCollection,
           --livre.seriel								seriel, 	
           livreEdition.EAN13						ean13, 	
           --livreDistribution.codeDistributeur		chronoVolumen,
           livreDistribution.code_ITF				chronoInterforum,
           livreEdition.codePubli					codePublicationAX,
           --livreEdition.codeEdition					codeEditionAX,   
		   livre.titre1+case when article1.nom is not null then ' ('+rtrim(article1.nom)+')' else '' end titre1, 
		   livre.titre2+case when article2.nom is not null then ' ('+rtrim(article2.nom)+')' else '' end titre2, 
		   livre.surTitre					surTitre, 
		   livre.auteurs					auteurs, 
		   --livre.auteurRecherche			triAuteurs, 
		   --faconnage.nom					faconnage,
		   --typoAchat.nom					typoAchat,
		   --typoAchat.achat					achat,
		   --typeEdition.nom					typeEdition,
		   --livre.confidentiel				confidentiel,
		   --livre.dateParution				dateParution,
		   --isnull(isnull(livreComptabilite.dateParutionCDG,livre.dateParution),offices.miseEnVente) dateParutionCDG,
		   --livreJuridique.fk_oeuvre,oeuvre.codeOeuvre CodeOeuvre,
		   --livreCommercial.prixTTC			prixTTC, 
           --livreCommercial.prixHT			prixHT, 
		   --livreEdition.Prix_HT_Cess		prixHTCession,
		   --typeProduit.codeTypeProduit		codeTypeProduit,
		   --typeProduit.nom					typeProduit,
		   --typeProduit.tauxTVA				tva,
		   --livreProduction.largeur			largeur, 
           --livreProduction.hauteur			hauteur, 
           --livreProduction.epaisseur		epaisseur, 
           --livreProduction.pages			pages, 
           --livreProduction.pagesHorsTexte	pagesHT, 
           --livre.tome						tome, 
           --livre.volume						volume, 
		   --[typeCateDepre].code				codeCategDepre,
		   --[typeCateDepre].Libelle			nomCategDepre,
		   --pyramide_ITF.codeEditeur_ITF		codePyramideEditeur,
		   --pyramide_ITF.codeSecteur_ITF		codePyramideSecteur,
		   --pyramide_ITF.codeCategorie_ITF	codePyramideCategorie,
		   --pyramide_ITF.nomEditeur_ITF		nomPyramideEditeur,
		   --pyramide_ITF.nomSecteur_ITF		nomPyramideSecteur,
		   --pyramide_ITF.nomCategorie_ITF	nomPyramideCategorie,
		   --livreEdition.annulation			annulation,	
		   --case when livreNumerique.fk_livre is null then 0 else 1 end isLivreNumeric,
		   --societe.nom						nomSocieteExploitation,
		   --societe.societeAX				codeSocieteExploitationAX,
		   --socCA.nom						nomSocieteCA,
		   --socCA.societeAX					codeSocieteCAAX,
		   --departementEditoNiv2.pk_departementEditoNiv2			pkPole,
		   --departementEditoNiv2.nom			nomPole,
		   --departementEditoNiv2.tri			triPole,
		   --departementEditoNiv1.pk_departementEditoNiv1			pkEntite,
		   --departementEditoNiv1.nom			nomEntite,
		   --departementEditoNiv1.tri			triEntite,
		   --'-'								nomTypeEntite,
		   --societe.interfaceAX				societeInterfaceAX,
		   --livreComptabilite.isPartenariat  isPartenariat,
		   /*(select p1.fk_livre 
		    from LivreNumerique p1
		     inner join LivrePapierNumerique p2 on p1.pk_livreNumerique=p2.Fk_LivreNumerique
		     inner join LivrePapierNumerique p3 on p3.Fk_LivrePapier = p2.Fk_LivrePapier
		     inner join LivreNumerique p4 on p3.Fk_LivreNumerique = p4.pk_livreNumerique
		    where p1.Fk_NumeriqueModele=6 
		      and p4.Fk_Livre = livre.pk_livre) PkLivrePNB,
			*/
		   --livreDistribution.poids,
		   --livreDistribution.codeRemplacement IdIntranetRemplacant,
		   typeFab.entiteFab,
		   typeFab.typeFabName
		   --,
		   --isnull(livreCommercial.okAnnonce,0) okAnnonce,
		   --livreCommercial.OfficeAnnonce,
		   --isnull(livreCommercial.okconfirmation,0) okConfirmation,
		   --livreCommercial.dateOfficeConfirmation,
		   --livreProgrammation.office,
		   --livre.achat libAchat,
		   --livre.abandonne,
		   --livreDistribution.date_Immat_ITF,
		   --livreCommercial.interdit_ITF,
		   --causeNotation.nom causeNotation,
		   --livreDistribution.dateLimiteAcceptRetour_ITF,
		   --livreDistribution.dateFinCommerce_ITF,
		   --livreCommercial.dateFinCommerce,
		   --livreCommercial.dateLimiteAcceptRetour
FROM       Reflmg.dbo.livre livre
           INNER JOIN Reflmg.dbo.livreCommercial ON livre.pk_livre = livreCommercial.fk_livre 
           left outer join Reflmg.dbo.themeDilicom on livreCommercial.CLIL = themeDilicom.codeCLIL
           INNER JOIN Reflmg.dbo.livreEdition ON livre.pk_livre = livreEdition.fk_livre 
           INNER JOIN Reflmg.dbo.livre_collection ON livre.pk_livre = livre_collection.fk_livre 
           INNER JOIN Reflmg.dbo.livreProduction ON livre.pk_livre = livreProduction.fk_livre 
           --INNER JOIN livreJuridique ON livre.pk_livre = livreJuridique.fk_livre 
           INNER JOIN Reflmg.dbo.collection ON livre_collection.fk_collection = collection.pk_collection 
           INNER JOIN Reflmg.dbo.editeur ON collection.fk_editeur = editeur.pk_editeur
           --INNER JOIN livreComptabilite ON livre.pk_livre = livreComptabilite.fk_livre 
           --INNER JOIN typeProduit ON livreCommercial.fk_typeProduit = typeProduit.pk_typeProduit 
           --INNER JOIN typeEdition ON livre.fk_typeEdition = typeEdition.pk_typeEdition 
           --INNER JOIN typoAchat ON typeEdition.fk_typoAchat = typoAchat.pk_typoAchat 
           --INNER JOIN societe ON societe.pk_societe = livreEdition.fk_societe 
           --LEFT OUTER JOIN societe as socCA ON editeur.fk_societe=socCA.pk_societe
           --LEFT OUTER JOIN oeuvre ON livreJuridique.fk_oeuvre = oeuvre.pk_Oeuvre
           --LEFT OUTER JOIN departementEdito ON livre.fk_departementEdito = departementEdito.pk_departementEdito
           --LEFT OUTER JOIN departementEditoNiv2 ON departementEdito.fk_departementEditoNiv2 = departementEditoNiv2.pk_departementEditoNiv2
           --LEFT OUTER JOIN departementEditoNiv1 ON departementEditoNiv2.fk_departementEditoNiv1 = departementEditoNiv1.pk_departementEditoNiv1
           --LEFT OUTER JOIN typeEntite ON entite.fk_typeEntite = typeEntite.pk_typeEntite
           LEFT OUTER JOIN Reflmg.dbo.article1 ON livre.fk_article1 = article1.pk_article1
		   LEFT OUTER JOIN Reflmg.dbo.article2 ON livre.fk_article2 = article2.pk_article2
		   LEFT OUTER JOIN Reflmg.dbo.livreDistribution ON livre.pk_livre = livreDistribution.fk_livre 
		   --LEFT OUTER JOIN typeCateDepre ON livreEdition.cateFiscaleDepre = typeCateDepre.code
		   --LEFT OUTER JOIN pyramide_ITF ON livreCommercial.fk_pyramide_ITF = pyramide_ITF.pk_pyramide_ITF
		   --LEFT OUTER JOIN livreNumerique ON livreNumerique.fk_livre = livre.pk_livre
		   --LEFT OUTER JOIN faconnage ON livreProduction.fk_faconnage = faconnage.pk_faconnage
		   --LEFT OUTER JOIN offices ON livreCommercial.dateOfficeConfirmation=offices.office
		   LEFT OUTER JOIN Reflmg.dbo.typeFab ON typeFab.pk_typeFab = livreProduction.fk_typeFab
		   --LEFT OUTER JOIN livreProgrammation on livreProgrammation.fk_livre=livre.pk_livre
		   --LEFT OUTER JOIN causeNotation on livreDistribution.fk_causeNotation_ITF = causeNotation.pk_causeNotation
WHERE livreEdition.annulation=0