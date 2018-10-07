
-- =============================================
-- Author:		aimad Hakkoum
-- Create date: 15/02/2018
-- Description:	Procedure d'import des données depuis les tables tmp de 4D vers les tables finales du modele LMG-FAB
-- =============================================
CREATE PROCEDURE [dbo].[import_4d_data]
AS
BEGIN
	SET NOCOUNT on;

/***************** vider et sauvegarder les données existantes   *****************/
	print 'truncate tables'

	--sauvegarder les lots de l'intranet (les offices)
	select * into #lotIntranet FROM lot
	where fk_offices is not null

	DELETE FROM lot
	DBCC CHECKIDENT ('dbo.lot',RESEED, 0)
	DELETE FROM article
	DBCC CHECKIDENT ('dbo.article',RESEED, 0)
	DELETE FROM machineGamme
	DBCC CHECKIDENT ('dbo.machineGamme',RESEED, 0)
	DELETE FROM machine
	DBCC CHECKIDENT ('dbo.machine',RESEED, 0)
	DELETE FROM prestation
	DBCC CHECKIDENT ('dbo.prestation',RESEED, 0)
	DELETE FROM tiers
	DBCC CHECKIDENT ('dbo.tiers',RESEED, 0)
	DELETE FROM produitGamme
	DBCC CHECKIDENT ('dbo.produitGamme',RESEED, 0)
	DELETE FROM produit
	DBCC CHECKIDENT ('dbo.produit',RESEED, 0)

	DELETE FROM elPapier
	DELETE FROM elFaconnage
	DELETE FROM elImpositionCahier
	DELETE FROM elImpositionFeuille
	DELETE FROM elImpression
	DELETE FROM element
	DBCC CHECKIDENT ('dbo.element',RESEED, 0)	
	DELETE FROM devis
	DBCC CHECKIDENT ('dbo.devis',RESEED, 0)	
	DELETE FROM composant
	DBCC CHECKIDENT ('dbo.composant',RESEED, 0)
	DELETE FROM dossier
	DBCC CHECKIDENT ('dbo.dossier',RESEED, 0)
	
	DELETE FROM paramDetail
	DBCC CHECKIDENT ('dbo.paramDetail',RESEED, 0)
	DELETE FROM paramTable
	DBCC CHECKIDENT ('dbo.paramTable',RESEED, 0)
	DELETE FROM collectionTechnique
	DBCC CHECKIDENT ('dbo.collectionTechnique',RESEED, 0)

/***************** Parametres   *****************/
	--- [paramTable]
	INSERT INTO [paramTable]
			   ([code]
			   ,[libelleLong]
			   ,[libelleCourt])
	SELECT LTRIM(Rtrim([Code_Titre]))
		  ,LTRIM(Rtrim([Definition]))
		  ,LTRIM(Rtrim([Definition]))
	  FROM [tmp_param_titre]
	  where Code_Titre is not null and Code_Titre !='' and Code_Titre !='COLL' 
	Union select 'TYAC','Typologie achat','Typologie achat'
			   
	print 'insert into [paramTable]: ' + STR(@@ROWCOUNT)

	----[paramDetail] (673 elements sans group by --> 669 avec le group by)
	INSERT INTO [paramDetail]
			   ([fk_paramTable]
			   ,[code]
			   ,[libelleLong]
			   ,[libelleCourt]
			   ,[tri])
	SELECT par.pk_paramTable
		  ,LTRIM(Rtrim(det.Code))
		  ,MAX(LTRIM(Rtrim(det.Valeur_Det)))
		  ,MAX(LTRIM(Rtrim(det.Valeur_Det)))
		  ,MAX([Tri_Det])
	  FROM [tmp_param_det] det
	  inner join [paramTable] par on  LTRIM(Rtrim(det.Code_Titre)) = par.code
	  group by par.pk_paramTable, LTRIM(Rtrim(det.Code))
	Union   
	 	SELECT par.pk_paramTable
		  ,LTRIM(Rtrim(det.codeTypoAchat))
		  ,LTRIM(Rtrim(det.nom))
		  ,LTRIM(Rtrim(det.nom))
		  ,0
	  FROM [Reflmg].[dbo].typoAchat det
	  inner join [paramTable] par on  par.code = 'TYAC'

	print 'insert into [paramDetail]: ' + STR(@@ROWCOUNT)

	-- Supprimer les lots avec un fk_office = null (qui ne viennent pas de la base réf)
	print 'mettre à jour la valeur par defaut de paramTable.codeDefaut'

	Update paramTable
	set codeDefaut = LTRIM(Rtrim(det.Code))
	from paramTable par ,tmp_param_titre par_tmp, tmp_param_det det 
	where par.code = LTRIM(Rtrim(par_tmp.Code_Titre)) and  par.codeDefaut is null and par_tmp.Code_Titre = det.Code_Titre
	and LTRIM(Rtrim(par_tmp.Valeur_Defaut)) =  LTRIM(Rtrim(det.Code))

	Update paramTable
	set codeDefaut = LTRIM(Rtrim(det.Code))
	from paramTable par ,tmp_param_titre par_tmp, tmp_param_det det 
	where par.code = LTRIM(Rtrim(par_tmp.Code_Titre)) and   (par.codeDefaut is null or par.codeDefaut ='') and par_tmp.Code_Titre = det.Code_Titre
	and (LTRIM(Rtrim(par_tmp.Valeur_Defaut)) =  LTRIM(Rtrim(det.Valeur_Assoc1)) or LTRIM(Rtrim(par_tmp.Valeur_Defaut)) =  LTRIM(Rtrim(det.Valeur_Assoc2)))
	and LTRIM(Rtrim(par_tmp.Valeur_Defaut))!=''

	select par.pk_paramTable, det.pk_paramDetail, par.code as code_param, det.code as code_det 
	into #paramList
	from paramTable par
	 join paramDetail det on det.fk_paramTable = par.pk_paramTable
	 
	 	----[collectionTechnique]
	 --recuperer la liste des collections et supprimer les doublons dans le code	
	SELECT LTRIM(Rtrim(det.Code)) as Code
		  ,MAX(LTRIM(Rtrim(det.Valeur_Det))) as Valeur_Det
		  ,MAX([Tri_Det]) as Tri_Det
		  ,MAX(LTRIM(Rtrim(det.Valeur_Assoc))) as Valeur_Assoc
		  ,MAX(LTRIM(Rtrim(det.Valeur_Assoc1))) as Valeur_Assoc1
		  ,MAX(replace(LTRIM(Rtrim(det.Valeur_Assoc2)),'/','')) as Valeur_Assoc2
		  ,'000' as P1,'000' as P2,'000' as P3,'000' as P4,'000' as P5,'000' as P6
		  into #tmpColl
	  FROM [tmp_param_det] det
	  where Code_Titre='COLL'
	  group by det.Code
	  
	  Update #tmpColl
	  Set P1 = LEFT(Valeur_Assoc2,3)
	  ,P2 = SUBSTRING(Valeur_Assoc2,4,3)
	  ,P3 = SUBSTRING(Valeur_Assoc2,7,3)
	  ,P4 = SUBSTRING(Valeur_Assoc2,10,3)
	  ,P5 = SUBSTRING(Valeur_Assoc2,13,3)
	  ,P6 = SUBSTRING(Valeur_Assoc2,16,3)
	  
	INSERT INTO collectionTechnique
           ([code]
           ,[nom]
           ,[tri]
           ,[fk_paramDetail_gamm]
           ,[okActif]
           ,[fk_paramDetail_pell_couv]
           ,[fk_paramDetail_vern_couv]
           ,[fk_paramDetail_pell_couvr]
           ,[fk_paramDetail_vern_couvr]
           ,[fk_paramDetail_pell_jaquette]
           ,[fk_paramDetail_vern_jaquette])
	SELECT det.Code
		  ,det.Valeur_Det
		  ,Tri_Det
		  ,par_gamme.pk_paramDetail
		  ,1
		  ,par_pell_couv.pk_paramDetail
		  ,par_ver_couv.pk_paramDetail
		  ,par_pell_couv_rabat.pk_paramDetail
		  ,par_ver_couv_rabat.pk_paramDetail
		  ,par_pell_jaq.pk_paramDetail
		  ,par_ver_jaq.pk_paramDetail
	  FROM #tmpColl det
	  	left join #paramList par_gamme on par_gamme.code_param ='GAMM' and par_gamme.code_det = LTRIM(Rtrim(det.Valeur_Assoc1)) 
	  	left join #paramList par_pell_couv on par_pell_couv.code_param ='PELL' and par_pell_couv.code_det = P1
	  	left join #paramList par_ver_couv on par_ver_couv.code_param ='VERN' and par_ver_couv.code_det = P2
	  	left join #paramList par_pell_couv_rabat on par_pell_couv_rabat.code_param ='PELL' and par_pell_couv_rabat.code_det = P3 
	  	left join #paramList par_ver_couv_rabat on par_ver_couv_rabat.code_param ='VERN' and par_ver_couv_rabat.code_det = P4
	  	left join #paramList par_pell_jaq on par_pell_jaq.code_param ='PELL' and par_pell_jaq.code_det = P5
	  	left join #paramList par_ver_jaq on par_ver_jaq.code_param ='VERN' and par_ver_jaq.code_det = P6

	print 'insert into [collectionTechnique]: ' + STR(@@ROWCOUNT)

/***************** Lots   *****************/
	-- inserer les lots de l'intranet
	--INSERT INTO [lot]
	--   ([nomLot] ,[fk_paramDetail_Proc],[codeLot],[fk_offices],[dateArriveeLivres],[dateMiseEnVente],[enCours])
	--   select [nomLot] ,paramProc.pk_paramDetail,[codeLot],[fk_offices],[dateArriveeLivres],[dateMiseEnVente],[enCours] from  #lotIntranet
	--   left join #paramList paramProc on paramProc.code_param ='PROC' and paramProc.code_det = 'Nouveauté'

	--print 'insert into [lot] de refLmg.offices: ' + STR(@@ROWCOUNT)
	
	-- importer les lots depuis la table tmp_lot  
	INSERT INTO [lot]
	   ([nomLot] ,fk_paramDetail_Proc,[codeLot],[fk_offices],[dateArriveeLivres],[dateMiseEnVente],[enCours]) 
	SELECT substring([Libelle],0, 20)
		,det.pk_paramDetail,
		LTRIM(Rtrim(Code_Lot)), 
		null,
		CASE WHEN (ISDATE([Date_Livres]) = 1)
			THEN CONVERT(datetime, [Date_Livres], 103)
			ELSE null
			END,
		CASE WHEN (ISDATE([Date_Vente]) = 1)
			THEN CONVERT(datetime, [Date_Vente], 103)
			ELSE null
			END,
		[En_Cours]
	 FROM tmp_lot left join #paramList det on det.code_param ='PROC'
	  and det.code_det = LTRIM(Rtrim(tmp_lot.Code_Processus)) 
	 
	 
	print 'insert into [lot] de tmp_lot: ' + STR(@@ROWCOUNT)
	
/***************** articles   *****************/
		
	INSERT INTO [article]
           ([idIntranet]
           ,[fk_paramDetail_edit]
           ,[titre]
           ,[fk_paramDetail_pres]
           ,[fk_paramDetail_form]
           ,[hauteur]
           ,[largeur]
           ,[nbPages]
           ,[nbSignesCalibres]
           ,[nbSignesEvalues]
           ,[observation]
           ,[nbPagesHorsTexte]
           ,[nbPagesHorsTexteDef]
           ,[nbPagesInterieurDef]
           ,fk_paramDetail_tyac
           ,fk_paramDetail_gamm)
	SELECT art.Code_Article
		  ,par_edit.pk_paramDetail
		  ,Titre
		  ,par_pres.pk_paramDetail --[fk_paramDetail_pres]
		  ,par_form.pk_paramDetail --[fk_paramDetail_form]
		  ,CONVERT(numeric(5,2), Replace(Hauteur,',','.'))  --[hauteur]
		  ,CONVERT(numeric(5,2), Replace(Largeur,',','.'))   --[largeur]
		  ,Nbre_Pages --[nbPages]
		  ,Sig_Calib --[nbSignesCalibres]
		  ,Sig_eval --[nbSignesEvalues]
		  ,ISNULL(Observation, '') + ISNULL('<br/>' + Annexe1, '') + ISNULL('<br/>' + Annexe2, '') + ISNULL('<br/>' + Annexe3, '')
		  ,Nb_Pages_Ht --[nbPagesHorsTexte]
		  ,Nb_PagHtx_Def --[nbPagesHorsTexteDef]
		  ,Nb_Pages_def --[nbPagesInterieurDef]
		  ,par_tyac.pk_paramDetail --fk_paramDetail_tyac
		  ,par_gamme.pk_paramDetail --fk_paramDetail_gamm
	FROM [tmp_articles] art 
		left join #paramList par_edit on par_edit.code_param ='EDIT' and par_edit.code_det = LTRIM(Rtrim(art.Sigle_Editeur)) 
		left join collectionTechnique coll on coll.code =  LTRIM(Rtrim(art.[Collection])) 
		left join #paramList par_pres on par_pres.code_param ='PRES' and par_pres.code_det = LTRIM(Rtrim(art.Code_Present)) 
		left join #paramList par_form on par_form.code_param ='FORM' and par_form.code_det = LTRIM(Rtrim(art.Code_Format)) 
		left join [Reflmg].[dbo].[livreDistribution] livreD on livreD.fk_livre = art.Code_Article
		left join [Reflmg].[dbo].livre livre on livre.pk_livre = livreD.fk_livre
		left join [Reflmg].[dbo].typeEdition typeEd on typeEd.pk_typeEdition = livre.fk_typeEdition
		left join [Reflmg].[dbo].typoAchat typo on typo.pk_typoAchat = typeEd.fk_typoAchat
		left join #paramList par_tyac on par_tyac.code_param ='TYAC' and par_tyac.code_det = typo.codeTypoAchat
		left join #paramList par_gamme on par_gamme.code_param ='GAMM' and par_gamme.code_det = LTRIM(Rtrim(art.LG_ou_P)) 
		
	print 'insert into [article]: ' + STR(@@ROWCOUNT)	
	
	update article
	set Hauteur = Hauteur*10 , Largeur = Largeur*10
		
/***************** tiers, prestations, machines   *****************/
		
	INSERT INTO [tiers]
           ([fk_paramDetail_type]
           ,[nom]
           ,[adresse1]
           ,[adresse2]
           ,[adresse3]
           ,[codePostal]
           ,[ville]
           ,[pays]
           ,[nomContact]
           ,[telephone]
           ,[codeFournisseurERP]
           ,[codeEntrepotERP]
           ,[signatureFicheTechnique]
           ,[texteCommande]
           ,[actif]
           ,[adresseEmail])
	SELECT par_type.pk_paramDetail
			,tiers.Nom
			,tiers.Adresse1
			,tiers.Adresse2
			,tiers.Adresse3
			,tiers.Code_Postal
			,tiers.Ville
			,tiers.Pays
			,tiers.Nom_Contact
			,tiers.Telephone
			,null --[codeFournisseurERP]
			,null --[codeEntrepotERP]
			,tiers.Contact_Fab -- [signatureFicheTechnique]
			,tiers.libelle_cde --[texteCommande]
			,tiers.Actif --[actif]
			, null --[adresseEmail]		  
	  FROM [tmp_tiers] tiers
	  	left join #paramList par_type on par_type.code_param ='TYPE' and par_type.code_det = LTRIM(Rtrim(tiers.TypeTiers)) 

	print 'insert into [tiers]: ' + STR(@@ROWCOUNT)	
	
	INSERT INTO [prestation]
		   ([fk_tiers]
		   ,[fk_paramDetail_trfo]
		   ,[tauxHoraire])
	SELECT tiers.pk_tiers
	  ,par_trfo.pk_paramDetail
	  ,CONVERT(numeric(9,2), Replace(Taux,',','.')) --[tauxHoraire]
	FROM [tmp_prestations] pres 
	left join tiers on LTRIM(Rtrim(tiers.nom)) = LTRIM(Rtrim(pres.Nom))
	left join #paramList par_trfo on  par_trfo.code_det = LTRIM(Rtrim(pres.Type_Travail))  and par_trfo.code_param ='TRFO'
	where pres.Type_Travail in ('IMP BANDE','IMP HTX','IMP JAQ','IMPRIMEUR')

	print 'insert into [prestation]: ' + STR(@@ROWCOUNT)	

	INSERT INTO machine
           ([nom]
           ,[tournees]
           ,[roto]
           ,[coming]
           ,[cameron]
           ,[fausseCoupe]
           ,[fk_prestation]
           ,[sousMultiplesCahiers])
	SELECT LTRIM(Rtrim([Intitule_Tarif]))
		,CASE WHEN (LTRIM(Rtrim(S_Multiple)) = '')
			THEN Tournees
			ELSE Tournees +'-' + S_Multiple
			END --[tournees]
		,Roto
		,Coming
		,Cameron
		,CONVERT(numeric(9,2), Replace(Fausse_Coupe,',','.')) --[fausseCoupe]
		,pres.pk_prestation -- [fk_prestation]
		,S_Multi_Cah --[sousMultiplesCahiers]
	FROM tmp_tarifs tarif
	inner join tmp_prestations presTmp on presTmp.Id_Prestation = tarif.Id_Prestation and  presTmp.Type_Travail in ('IMP BANDE','IMP HTX','IMP JAQ','IMPRIMEUR')
	inner join tiers on LTRIM(Rtrim(tiers.nom)) = LTRIM(Rtrim(presTmp.Nom))
	inner join prestation pres on pres.fk_tiers = tiers.pk_tiers
	inner join #paramList par_trfo on  par_trfo.code_det = presTmp.Type_Travail  and par_trfo.code_param ='TRFO' and pres.fk_paramDetail_trfo = par_trfo.pk_paramDetail
	order by [Intitule_Tarif]
  
	print 'insert into [machine]: ' + STR(@@ROWCOUNT)	
	
	INSERT INTO [machineGamme]
           ([fk_machine]
           ,[fk_paramDetail_gamm]
           ,[codeDatexp]
           ,[actif])
	SELECT machine.pk_machine
		,par_gamm.pk_paramDetail
		,tarif.Feuille_Excel_L --[codeDatexp]
		,1 --actif
	FROM tmp_tarifs tarif
	inner join tmp_prestations presTmp on presTmp.Id_Prestation = tarif.Id_Prestation and  presTmp.Type_Travail in ('IMP BANDE','IMP HTX','IMP JAQ','IMPRIMEUR')
	inner join tiers on LTRIM(Rtrim(tiers.nom)) = LTRIM(Rtrim(presTmp.Nom))
	inner join prestation pres on pres.fk_tiers = tiers.pk_tiers
	inner join #paramList par_trfo on  par_trfo.code_det = presTmp.Type_Travail  and par_trfo.code_param ='TRFO' and pres.fk_paramDetail_trfo = par_trfo.pk_paramDetail
	inner join machine on machine.fk_prestation = pres.pk_prestation and LTRIM(Rtrim(machine.nom)) = LTRIM(Rtrim(tarif.Intitule_Tarif))
	inner join #paramList par_gamm on  par_gamm.code_det = 'LG'  and par_gamm.code_param ='GAMM'
	where tarif.Gamme_LG = 1
	Union
	SELECT machine.pk_machine
		,par_gamm.pk_paramDetail
		,tarif.Feuille_Excel_P --[codeDatexp]
		,1 --actif
	FROM tmp_tarifs tarif
	inner join tmp_prestations presTmp on presTmp.Id_Prestation = tarif.Id_Prestation and  presTmp.Type_Travail in ('IMP BANDE','IMP HTX','IMP JAQ','IMPRIMEUR') 
	inner join tiers on LTRIM(Rtrim(tiers.nom)) = LTRIM(Rtrim(presTmp.Nom))
	inner join prestation pres on pres.fk_tiers = tiers.pk_tiers
	inner join #paramList par_trfo on  par_trfo.code_det = presTmp.Type_Travail  and par_trfo.code_param ='TRFO' and pres.fk_paramDetail_trfo = par_trfo.pk_paramDetail
	inner join machine on machine.fk_prestation = pres.pk_prestation and LTRIM(Rtrim(machine.nom)) = LTRIM(Rtrim(tarif.Intitule_Tarif))
	inner join #paramList par_gamm on  par_gamm.code_det = 'P'  and par_gamm.code_param ='GAMM'
	where tarif.Gamme_Poche = 1
	
	print 'insert into [machineGamme]: ' + STR(@@ROWCOUNT)	

/***************** produits   *****************/
	
	SET IDENTITY_INSERT dbo.produit ON;  
	
	INSERT INTO [produit]
           (pk_produit
           ,[famille]
           ,[intitule]
           ,[grammage]
           ,[prixKg]
           ,[main]
           ,[largeurMm]
           ,[hauteurMm]
           ,[okRoto]
           ,[okFeuille]
           ,[definiParUtil]
           ,fk_paramDetail_qual)
	SELECT [Id_Produit]
		  ,[Famille]
		  ,[Intitule]
		  ,CONVERT(numeric(9,2), Replace([Grammage],',','.'))
		  ,CONVERT(numeric(9,2), Replace([Prix],',','.'))
		  ,CONVERT(numeric(9,2), Replace([Main],',','.'))
  		  ,CONVERT(numeric(9,2), Replace([Larg],',','.')) *10
  		  ,CONVERT(numeric(9,2), Replace([Hauteur],',','.')) *10
  		  ,CASE WHEN LTRIM(Rtrim(Roto_Feuilles)) ='1' THEN 0
				 ELSE 1
			END  --[okRoto]
		  ,[Roto_Feuilles] -- [okFeuille]
		  ,[Def_Par_User]--[definiParUtil]
		  ,par_qual.pk_paramDetail --fk_paramDetail_qual
	  FROM [tmp_produits] prod
	  	left join #paramList par_qual on  par_qual.code_det = prod.qualite  and par_qual.code_param ='QUAL'
	  order by [Id_Produit]

	print 'insert into [produit]: ' + STR(@@ROWCOUNT)	

	INSERT INTO [produitGamme]
           ([fk_produit]
           ,[fk_paramDetail_gamm])
	select Id_Produit, par_gamm.pk_paramDetail from tmp_produits
	inner join #paramList par_gamm on  par_gamm.code_det = 'LG'  and par_gamm.code_param ='GAMM'
	where Gamme_LG = 1
	Union
	Select Id_Produit, par_gamm.pk_paramDetail from tmp_produits
	inner join #paramList par_gamm on  par_gamm.code_det = 'P'  and par_gamm.code_param ='GAMM'
	where Gamme_P = 1

	print 'insert into [produitGamme]: ' + STR(@@ROWCOUNT)	


/***************** dossiers   *****************/

	--MAJ codeTirageEdition : maj en masse pour optimisation
	update 	tmp_tirages
	set codeTirageEdition  = livT.codeTirageEdition, [Collection] = tmp_articles.[Collection]
	from tmp_tirages
	left join [Reflmg].[dbo].[livreTirages] livT on livT.fk_livre = LTRIM(Rtrim(tmp_tirages.Code_Article)) and livT.numeroTirage = Num_Tirage
	and livT.suite = LTRIM(Rtrim(tmp_tirages.Num_Suite)) and livT.fk_destinataire = 1
	left join tmp_articles on tmp_articles.Code_Article = livT.fk_livre
	where ISNUMERIC(Num_Tirage) = 1 
		
	--parcourir la table tirage pour creer les dossiers
	SELECT ROW_NUMBER() OVER(ORDER BY Id_Tirage) AS id, tmp_tirages.*
	INTO #tempTirages
	FROM tmp_tirages
	where Id_Tirage > 0  --and Id_Tirage in (44433,27910,22243,14743) -- cas de test

	DECLARE @IndexTirage INT
	SET @IndexTirage = 1
	
	DECLARE @CountTirage INT
	SELECT @CountTirage = MAX(id) FROM #tempTirages
	
	Declare @pk_dossier int
	Declare @pk_composant int
	Declare @pk_devis int
	Declare @pk_element int
	declare @couv_a_Rabats int
	
	print 'nombre de tirages à insérer: ' + STR(@CountTirage)	
	
	WHILE(@IndexTirage <= @CountTirage)
	BEGIN
		DECLARE @Id_Tirage INT
		
		SELECT * into #tempTiragesRow
		FROM #tempTirages
		WHERE id = @IndexTirage
		
		SELECT 
			@Id_Tirage = Id_Tirage , @couv_a_Rabats = Couv_a_Rabats
		FROM #tempTiragesRow
		
	print 'insertion du tirage : ' + STR(@Id_Tirage)	
			
	INSERT INTO [dossier]
		   ([idTirage]
		   ,[fk_lot]
		   ,prixTTC
		   ,[okCoffret]
		   ,[filierePrincipale]
		   ,[fk_paramDetail_proc]
		   ,idTirage4d
		   ,qteTirage)
	select  codeTirageEdition
			,lot.pk_lot
			,CONVERT(numeric(9,2), Replace(tir.Prix,',','.'))
			,tir.Coffret
			,Filiere_Princ
			,par_proc.pk_paramDetail
			,@Id_Tirage
			,Qte_Tirage
	from #tempTiragesRow tir
	left join #paramList par_proc on  par_proc.code_det = LTRIM(Rtrim(tir.code_processus))  and par_proc.code_param ='PROC'   
	left join lot on lot.codeLot = LTRIM(Rtrim(tir.Code_Lot))
		
	SET @pk_dossier = @@IDENTITY

	
	INSERT INTO composant
           ([fk_dossier]
           ,[nom])
	select @pk_dossier, null
	
	SET @pk_composant = @@IDENTITY
	
	INSERT INTO [devis]
           ([fichierFourni]
           ,[fk_composant]
           ,[okActif]
           ,[fk_paramDetail_stfb]
           ,[nbHeuresSuiviFab]
           ,[fk_paramDetail_sadm]
           ,fk_collectionTechnique)
	select tir.Disquette
		   ,@pk_composant
		   ,1
		   ,par_stfb.pk_paramDetail
		   ,tir.Heures_FP
		   ,par_sadm.pk_paramDetail
		   ,coll.pk_collectionTechnique
	from #tempTiragesRow tir
	left join #paramList par_stfb on  par_stfb.code_det = LTRIM(Rtrim(tir.Standard_FP))  and par_stfb.code_param ='STFB'
	left join #paramList par_sadm on  par_sadm.code_det = LTRIM(Rtrim(tir.Standard_ADM))  and par_sadm.code_param ='SADM'
	left join collectionTechnique coll on coll.code =  LTRIM(Rtrim(tir.[Collection])) 
	
	SET @pk_devis = @@IDENTITY

	/**** type des elements ****/	
	--pk_typeElement	code
	--1	TEXTE
	--2	ILLUSTRIN
	--3	COUV
	--4	COUVRB
	--5	JAQUETTE
	--6	HORSTEXTE
	--7	BANDE
	--8	FACONNAGE

	--parcourir les natures pour insérer les elements
	
	SELECT *
	INTO #tempNaturesRows
	from tmp_natures
	where Id_Tirage = @Id_Tirage
	
	SELECT ROW_NUMBER() OVER(ORDER BY nat.Id_Tirage) AS id, nat.*
	INTO #tempNatures
	from #tempNaturesRows nat

	DECLARE @IndexNature INT
	SELECT @IndexNature = MAX(id) FROM #tempNatures
	
	WHILE (@IndexNature != 0)
	BEGIN
		DECLARE @typeElement INT	
		DECLARE @codeNature nvarchar(50)	
		select  
		@codeNature = LTRIM(Rtrim(Code_Nature))
		,@typeElement = 
		CASE
		 WHEN LTRIM(Rtrim(Code_Nature)) IN ('FACON','INTEGRE','PAO','PH','REIMP') THEN 1
		 WHEN LTRIM(Rtrim(Code_Nature)) IN ( 'ILLUSTRIN' ) THEN 2
		 WHEN LTRIM(Rtrim(Code_Nature)) IN ( 'COUV' ) and @couv_a_Rabats = 0 THEN 3
		 WHEN LTRIM(Rtrim(Code_Nature)) IN ( 'COUV' ) and @couv_a_Rabats = 1 THEN 4
		 WHEN LTRIM(Rtrim(Code_Nature)) IN ( 'JAQUETTE' ) THEN 5
		 WHEN LTRIM(Rtrim(Code_Nature)) IN ( 'HORSTEXTE' ) THEN 6
		 WHEN LTRIM(Rtrim(Code_Nature)) IN ( 'BANDE' ) THEN 7
		 WHEN LTRIM(Rtrim(Code_Nature)) IN ( 'BROCHEUR','INTEGRA','RELIEUR' ) THEN 8
		END
		from #tempNatures
		where id = @IndexNature
		
		-- insérer l'element
		INSERT INTO [element]
		   ([fk_devis]
		   ,[fk_typeElement]
		   ,[fraisFixe]
		   ,[fraisVariable]
		   ,[portFixe]
		   ,[fk_tiers_fournisseur])
		select top 1 @pk_devis
		, @typeElement
		,CASE
		 WHEN LTRIM(Rtrim(Code_Nature)) IN ( 'BROCHEUR','INTEGRA','RELIEUR' ) THEN CONVERT(numeric(9,2), Replace(nat.Facon_FFix,',','.'))
		 ELSE 0
		END AS fraisFixe
		,CASE
		 WHEN LTRIM(Rtrim(Code_Nature)) IN ( 'BROCHEUR','INTEGRA','RELIEUR' ) THEN CONVERT(numeric(9,2), Replace(nat.Facon_FVar,',','.'))
		 ELSE 0
		END AS fraisVariable
		,CASE
		 WHEN LTRIM(Rtrim(Code_Nature)) IN ( 'BROCHEUR','INTEGRA','RELIEUR' ) THEN CONVERT(numeric(9,2), Replace(nat.Facon_Port,',','.'))
		 ELSE 0
		END AS portFixe
		,CASE
		 WHEN LTRIM(Rtrim(Code_Nature)) IN ( 'BROCHEUR','INTEGRA','RELIEUR' ) THEN fac.pk_tiers
		 ELSE four.pk_tiers
		END AS fk_tiers_fournisseur
		from #tempNatures nat
		left join tiers fac on fac.nom = LTRIM(Rtrim(nat.Faconnier))
		left join tiers four on fac.nom = LTRIM(Rtrim(nat.Fournisseur))
		where id = @IndexNature

		SET @pk_element = @@IDENTITY

	-- insérer le detail de l'element
	--  1: elPapier : on ajoute cet element si Papier_Ref n'est pas vide ou = 0

		IF EXISTS(SELECT * from #tempNatures where id = @IndexNature and LTRIM(Rtrim(Papier_Ref)) != '0' and LTRIM(Rtrim(Papier_Ref)) != '')
		BEGIN
		INSERT elPapier
				  ([pk_element]
				  ,[fk_produit]
				  ,[fk_paramDetail_qual]
				  ,[grammage]
				  ,[prix]
				  ,[main]
				  ,[largeurMm]
				  ,[hauteurMm])
		Select top 1 @pk_element
			  ,LTRIM(Rtrim(Papier_Ref)) --[fk_produit]:on a gardé le meme id de 4D sur la table produit
			  ,par_qual.pk_paramDetail
			  ,CASE
				WHEN ISNUMERIC(Replace(nat.Papier_Grammage,',','.'))=1 and LEN(nat.Papier_Grammage) <=7 THEN 
					CONVERT(numeric(9,2), Replace(nat.Papier_Grammage,',','.'))
				ELSE 
					NULL 
				END	
			  ,CASE
				WHEN ISNUMERIC(Replace(nat.Papier_Prix,',','.'))=1 and LEN(nat.Papier_Prix) <=7 THEN 
					CONVERT(numeric(9,2), Replace(nat.Papier_Prix,',','.'))
				ELSE 
					NULL 
				END	
			  ,CASE
				WHEN ISNUMERIC(Replace(nat.Papier_Main,',','.'))=1  and LEN(nat.Papier_Main) <=7 THEN 
					CONVERT(numeric(9,2), Replace(nat.Papier_Main,',','.'))
				ELSE 
					NULL 
				END	
			  ,CASE
				WHEN ISNUMERIC(Replace(nat.Papier_Larg,',','.'))=1 and LEN(nat.Papier_Larg) <=7 THEN 
					CONVERT(numeric(9,2), Replace(nat.Papier_Larg,',','.')) * 10
				ELSE 
					NULL 
				END	
			  ,CASE
				WHEN ISNUMERIC(Replace(nat.Papier_Haut,',','.'))=1 and LEN(nat.Papier_Haut) <=7 THEN 
					CONVERT(numeric(9,2), Replace(nat.Papier_Haut,',','.')) * 10
				ELSE 
					NULL 
				END	
			  from #tempNatures nat
			  left join #paramList par_qual on  par_qual.code_det = LTRIM(Rtrim(nat.Papier_Qual))  and par_qual.code_param ='QUAL'
			  where id = @IndexNature
		END

	--  2: Faconnage : on ajoute cet element si @typeElement = 8	(FACONNAGE)
		IF (@typeElement = 8)
		Begin
			--print 'ajouter un element faconnage'
			DECLARE @Code_Nature nvarchar(50)
			select @Code_Nature = CASE
				 WHEN LTRIM(Rtrim(Code_Nature)) IN ( 'BROCHEUR' ) THEN 'BROCHE'
				 WHEN LTRIM(Rtrim(Code_Nature)) IN ( 'INTEGRA' ) THEN 'INTEGRA'
				 ELSE null
			   END
			from #tempNatures
			where id = @IndexNature
		    
			INSERT INTO [elFaconnage]
			   ([pk_element]
			   ,[faconnerTout]
			   ,[fk_paramDetail_tbro]
			   ,[fk_produit_toile]
			   ,[fk_paramDetail_gard]
			   ,[quantite]
			   ,[valeurToile]
			   ,[ferADorer]
			   ,fk_tiers_faconnier)
			   select Top 1 @pk_element
					 ,nat.Facon_Tout
					 ,par_broche.pk_paramDetail
					 ,CASE
						 WHEN LTRIM(Rtrim(Type_Reliure)) != '0' and LTRIM(Rtrim(Type_Reliure)) != '' THEN Type_Reliure
						 ELSE null
					  END --[fk_produit_toile]
					 ,par_garde.pk_paramDetail
					 ,nat.Facon_Qte
					 ,CASE
						 WHEN LTRIM(Rtrim(Code_Nature)) = 'RELIEUR' THEN CONVERT(numeric(9,2), Replace(nat.Val_Toile,',','.'))
						 ELSE null
					  END --[valeurToile]
					 ,CASE
						 WHEN LTRIM(Rtrim(Code_Nature)) = 'RELIEUR' THEN CONVERT(numeric(9,2), Replace(nat.Fer_a_Dorer,',','.'))
						 ELSE null
					  END --[ferADorer]
					  ,tiers.pk_tiers
			   from #tempNatures nat
				  left join #paramList par_broche on  par_broche.code_det = @Code_Nature  and par_broche.code_param ='QUAL'
				  left join tiers on tiers.nom = LTRIM(Rtrim(nat.Faconnier))
				  left join #paramList par_garde on  par_garde.code_det = nat.Page_de_Garde  and par_garde.code_param ='GARD'
			  where nat.id = @IndexNature
		End
		
	--  3: elImpositionCahier : on ajoute cet element pour toutes les natures sauf PAO et PH
		IF (@codeNature not in ('PAO','PH'))
		Begin
		INSERT INTO [elImpositionCahier]
           ([pk_element]
           ,[nbPagesParCahier1]
           ,[nbPagesParCahier2]
           ,[nbPagesParCahier3]
           ,[nbPagesParCahier4]
           ,[nbCahiers1]
           ,[nbCahiers2]
           ,[nbCahiers3]
           ,[nbCahiers4])
		Select @pk_element
			  ,NPA_Cahier_1
			  ,NPA_Cahier_2
			  ,NPA_Cahier_3
			  ,NPA_Cahier_4
			  ,Nb_Cahiers_1
			  ,Nb_Cahiers_2
			  ,Nb_Cahiers_3
			  ,Nb_Cahiers_4
			  from #tempNatures nat
			  where id = @IndexNature
		END
		
	--  4: elImpositionFeuille : on ajoute cet element pour toutes les natures sauf BROCHEUR
		IF (@codeNature not in ('BROCHEUR'))
		Begin
			INSERT INTO [elImpositionFeuille]
           ([pk_element]
           ,[imposition64Pages]
           ,[nbPagesParFeuille1]
           ,[nbPagesParFeuille2]
           ,[nbPagesParFeuille3]
           ,[nbPagesChute]
           ,[nbFeuilles1] 
           ,[nbFeuilles2]
           ,[nbFeuilles3])
		Select @pk_element
			  ,CASE WHEN LTRIM(Rtrim(Impo_64P)) ='1' THEN 1
				 ELSE 0
				 END AS imposition64Pages
			  ,NPA_Feuille_1
			  ,NPA_Feuille_2
			  ,NPA_Feuille_3
			  ,Feuille_Chute
			  ,CONVERT(numeric(9,2), Replace(Nb_Feuilles_1,',','.'))
			  ,CONVERT(numeric(9,2), Replace(Nb_Feuilles_2,',','.'))
			  ,CONVERT(numeric(9,2), Replace(Nb_Feuilles_3,',','.'))
			  from #tempNatures nat
			  where id = @IndexNature
		END
		
	--  5: elImpression : on ajoute cet element pour toutes les natures sauf BROCHEUR et PAO
		IF (@codeNature not in ('BROCHEUR','PAO'))
		Begin
			INSERT INTO [elImpression]
           (pk_elImpression
           ,[fk_machine]
           ,[fk_paramDetail_pell]
           ,[fk_paramDetail_vern]
           ,[fk_paramDetail_coul_recto]
           ,[fk_paramDetail_coul_verso]
           ,[fk_paramdetail_type])
		Select top 1 @pk_element
			   ,machine.pk_machine
			   ,par_pell.pk_paramDetail
			   ,par_vern.pk_paramDetail
			   ,par_coulR.pk_paramDetail
			   ,par_coulV.pk_paramDetail
			   ,CASE
					WHEN LTRIM(Rtrim(Code_Nature)) in ('COUV','JAQUETTE') THEN par_couv.pk_paramDetail
					ELSE null
			  END --[fk_paramdetail_type]
			  from #tempNatures nat
			  left join tmp_tarifs tarif on tarif.Id_Tarif = nat.Machine 
			  left join tmp_prestations pres on tarif.Id_Prestation= pres.Id_Prestation
			  left join machine on LTRIM(Rtrim(tarif.Intitule_Tarif)) = machine.nom
			  left join #paramList par_pell on  par_pell.code_det = LTRIM(Rtrim(nat.Pelliculage))  and par_pell.code_param ='PELL'
			  left join #paramList par_vern on  par_vern.code_det = LTRIM(Rtrim(nat.Vernis))  and par_vern.code_param ='VERN'
			  left join #paramList par_coulR on  par_coulR.code_det = LTRIM(Rtrim(nat.Nb_Couleurs))  and par_coulR.code_param ='COUL'
			  left join #paramList par_coulV on  par_coulV.code_det = LTRIM(Rtrim(nat.Nb_Coul_Verso))  and par_coulV.code_param ='COUL'
			  left join #paramList par_couv on  par_couv.code_det = LTRIM(Rtrim(nat.Type_Couverture))  and par_couv.code_param ='COUV'
			  where id = @IndexNature
		END
		
		SET @IndexNature = @IndexNature -1

		END --end WHILE(@IndexNature != 0)

		drop table #tempNatures
		drop table #tempNaturesRows
		drop table #tempTiragesRow

		SET @IndexTirage = @IndexTirage + 1
		
	END --end WHILE(@IndexTirage <= @CountTirage)
	
	
	print 'fin chargement des tirages: '
	
	drop table #tempTirages
	drop table #paramList
	drop table #tmpColl
	drop table #lotIntranet

END

