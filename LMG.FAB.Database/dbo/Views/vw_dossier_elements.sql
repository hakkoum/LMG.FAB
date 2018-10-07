CREATE VIEW dbo.vw_dossier_elements
AS
SELECT     TOP (100) PERCENT dbo.dossier.pk_dossier, dbo.dossier.idTirage, dbo.dossier.idTirage4d, dbo.lot.nomLot, paramDetail_3.code AS type_processus, 
                      dbo.dossier.prixTTC, dbo.dossier.okCoffret, dbo.dossier.filierePrincipale, dbo.composant.pk_composant, dbo.devis.pk_devis, dbo.devis.okActif, 
                      dbo.collectionTechnique.code AS collection_code, dbo.collectionTechnique.nom AS collection_nom, paramDetail_4.code AS devis_Standard_FB, 
                      paramDetail_5.code AS devis_Standard_ADM, dbo.devis.nbHeuresSuiviFab, dbo.element.pk_element, dbo.element.fraisFixe, dbo.element.fraisVariable, 
                      dbo.element.portFixe, dbo.element.fk_tiers_fournisseur, dbo.tiers.nom AS nom_fournisseur, dbo.typeElement.code AS type_element, produit_1.intitule AS papier_type, 
                      dbo.elPapier.grammage AS papier_grammage, dbo.elPapier.prix AS papier_prix, dbo.elPapier.main AS papier_main, dbo.elPapier.largeurMm AS papier_largeurMm, 
                      dbo.elPapier.hauteurMm AS papier_hauteurMm, paramDetail_1.code AS papier_qual, dbo.elFaconnage.faconnerTout AS faconnage_faconnerTout, 
                      dbo.elFaconnage.quantite AS faconnage_quantite, dbo.elFaconnage.valeurToile AS faconnage_valeurToile, dbo.elFaconnage.ferADorer AS faconnage_ferADorer, 
                      paramDetail_2.code AS faconnage_type_brochage, dbo.produit.intitule AS faconnage_type_toile, dbo.elImpositionCahier.nbPagesParCahier1, 
                      dbo.elImpositionCahier.nbPagesParCahier2, dbo.elImpositionCahier.nbPagesParCahier3, dbo.elImpositionCahier.nbPagesParCahier4, 
                      dbo.elImpositionCahier.nbCahiers1, dbo.elImpositionCahier.nbCahiers2, dbo.elImpositionCahier.nbCahiers3, dbo.elImpositionCahier.nbCahiers4, 
                      dbo.elImpositionFeuille.imposition64Pages, dbo.elImpositionFeuille.nbPagesParFeuille1, dbo.elImpositionFeuille.nbPagesParFeuille2, 
                      dbo.elImpositionFeuille.nbPagesParFeuille3, dbo.elImpositionFeuille.nbPagesChute, dbo.elImpositionFeuille.nbFeuilles1, dbo.elImpositionFeuille.nbFeuilles2, 
                      dbo.elImpositionFeuille.nbFeuilles3, dbo.machine.nom AS impression_machine, dbo.elImpression.pk_elImpression, paramDetail_6.code AS impression_pellicule, 
                      paramDetail_7.code AS impression_vernie, paramDetail_8.code AS impression_couleur_recto, paramDetail_9.code AS impression_couleur_verso, 
                      dbo.paramDetail.code AS impression_type_couverture, dbo.devis.fk_collectionTechnique
FROM         dbo.collectionTechnique RIGHT OUTER JOIN
                      dbo.devis INNER JOIN
                      dbo.dossier INNER JOIN
                      dbo.composant ON dbo.dossier.pk_dossier = dbo.composant.fk_dossier ON dbo.devis.fk_composant = dbo.composant.pk_composant INNER JOIN
                      dbo.element ON dbo.devis.pk_devis = dbo.element.fk_devis INNER JOIN
                      dbo.paramDetail AS paramDetail_3 ON dbo.dossier.fk_paramDetail_proc = paramDetail_3.pk_paramDetail ON 
                      dbo.collectionTechnique.pk_collectionTechnique = dbo.devis.fk_collectionTechnique LEFT OUTER JOIN
                      dbo.paramDetail RIGHT OUTER JOIN
                      dbo.elImpression ON dbo.paramDetail.pk_paramDetail = dbo.elImpression.fk_paramdetail_type LEFT OUTER JOIN
                      dbo.paramDetail AS paramDetail_9 ON dbo.elImpression.fk_paramDetail_coul_verso = paramDetail_9.pk_paramDetail LEFT OUTER JOIN
                      dbo.paramDetail AS paramDetail_8 ON dbo.elImpression.fk_paramDetail_coul_recto = paramDetail_8.pk_paramDetail LEFT OUTER JOIN
                      dbo.paramDetail AS paramDetail_7 ON dbo.elImpression.fk_paramDetail_vern = paramDetail_7.pk_paramDetail LEFT OUTER JOIN
                      dbo.paramDetail AS paramDetail_6 ON dbo.elImpression.fk_paramDetail_pell = paramDetail_6.pk_paramDetail LEFT OUTER JOIN
                      dbo.machine ON dbo.elImpression.fk_machine = dbo.machine.pk_machine ON dbo.element.pk_element = dbo.elImpression.pk_elImpression LEFT OUTER JOIN
                      dbo.elImpositionCahier ON dbo.element.pk_element = dbo.elImpositionCahier.pk_element LEFT OUTER JOIN
                      dbo.elImpositionFeuille ON dbo.element.pk_element = dbo.elImpositionFeuille.pk_element LEFT OUTER JOIN
                      dbo.tiers ON dbo.element.fk_tiers_fournisseur = dbo.tiers.pk_tiers LEFT OUTER JOIN
                      dbo.paramDetail AS paramDetail_5 ON dbo.devis.fk_paramDetail_sadm = paramDetail_5.pk_paramDetail LEFT OUTER JOIN
                      dbo.paramDetail AS paramDetail_4 ON dbo.devis.fk_paramDetail_stfb = paramDetail_4.pk_paramDetail LEFT OUTER JOIN
                      dbo.lot ON dbo.dossier.fk_lot = dbo.lot.pk_lot LEFT OUTER JOIN
                      dbo.typeElement ON dbo.element.fk_typeElement = dbo.typeElement.pk_typeElement LEFT OUTER JOIN
                      dbo.elPapier LEFT OUTER JOIN
                      dbo.produit AS produit_1 ON dbo.elPapier.fk_produit = produit_1.pk_produit LEFT OUTER JOIN
                      dbo.paramDetail AS paramDetail_1 ON dbo.elPapier.fk_paramDetail_qual = paramDetail_1.pk_paramDetail ON 
                      dbo.element.pk_element = dbo.elPapier.pk_element LEFT OUTER JOIN
                      dbo.elFaconnage LEFT OUTER JOIN
                      dbo.produit ON dbo.elFaconnage.fk_produit_toile = dbo.produit.pk_produit LEFT OUTER JOIN
                      dbo.paramDetail AS paramDetail_2 ON dbo.elFaconnage.fk_paramDetail_tbro = paramDetail_2.pk_paramDetail ON 
                      dbo.element.pk_element = dbo.elFaconnage.pk_element
ORDER BY dbo.dossier.pk_dossier DESC

GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[49] 4[4] 2[16] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1[57] 4[16] 3) )"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 1
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "paramDetail"
            Begin Extent = 
               Top = 18
               Left = 2195
               Bottom = 137
               Right = 2395
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "elImpression"
            Begin Extent = 
               Top = 24
               Left = 1516
               Bottom = 188
               Right = 1735
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "paramDetail_9"
            Begin Extent = 
               Top = 275
               Left = 2193
               Bottom = 394
               Right = 2393
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "paramDetail_8"
            Begin Extent = 
               Top = 143
               Left = 2194
               Bottom = 262
               Right = 2394
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "paramDetail_7"
            Begin Extent = 
               Top = 187
               Left = 1937
               Bottom = 306
               Right = 2137
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "paramDetail_6"
            Begin Extent = 
               Top = 196
               Left = 1719
               Bottom = 315
               Right = 1919
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "machine"
            Begin Extent = 
               Top = 27
               Left = 1790
               Bottom = 184
               Right = 1990', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vw_dossier_elements';






GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "devis"
            Begin Extent = 
               Top = 3
               Left = 487
               Bottom = 207
               Right = 687
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "dossier"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 249
               Right = 238
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "composant"
            Begin Extent = 
               Top = 35
               Left = 270
               Bottom = 139
               Right = 470
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "element"
            Begin Extent = 
               Top = 19
               Left = 737
               Bottom = 190
               Right = 937
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "paramDetail_3"
            Begin Extent = 
               Top = 233
               Left = 0
               Bottom = 352
               Right = 200
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "elImpositionCahier"
            Begin Extent = 
               Top = 377
               Left = 606
               Bottom = 540
               Right = 806
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "elImpositionFeuille"
            Begin Extent = 
               Top = 378
               Left = 869
               Bottom = 543
               Right = 1069
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tiers"
            Begin Extent = 
               Top = 196
               Left = 687
               Bottom = 382
               Right = 895
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "paramDetail_5"
            Begin Extent = 
               Top = 197
               Left = 126
               Bottom = 316
               Right = 326
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "paramDetail_4"
            Begin Extent = 
               Top = 349
               Left = 84
               Bottom = 468
               Right = 284
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "lot"
            Begin Extent = 
               Top = 174
               Left = 272
               Bottom = 344
               Right = 472
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "typeElement"
            Begin Extent = 
               Top = 374
               Left = 1059
               Bottom = 537
               Right = 1261
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "elPapier"
            Begin Extent = 
               Top = 17
               Left = 1262
               Bottom = 203
               Right = 1462
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "produit_1"
            Begin Extent = 
               Top = 223
               Left = 1248
               Bottom = 342
               Right = 1448
            End
           ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vw_dossier_elements';








GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane3', @value = N' DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "paramDetail_1"
            Begin Extent = 
               Top = 201
               Left = 1487
               Bottom = 367
               Right = 1687
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "elFaconnage"
            Begin Extent = 
               Top = 116
               Left = 1021
               Bottom = 312
               Right = 1221
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "produit"
            Begin Extent = 
               Top = 389
               Left = 311
               Bottom = 530
               Right = 511
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "paramDetail_2"
            Begin Extent = 
               Top = 377
               Left = 1303
               Bottom = 496
               Right = 1503
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "collectionTechnique"
            Begin Extent = 
               Top = 243
               Left = 435
               Bottom = 362
               Right = 671
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
      PaneHidden = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 58
         Width = 284
         Width = 1500
         Width = 2205
         Width = 1500
         Width = 1995
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1995
         Width = 1980
         Width = 1800
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1965
         Width = 1500
         Width = 2160
         Width = 2055
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1875
         Width = 1680
         Width = 2235
         Width = 1980
         Width = 1500
         Width = 1500
         Width = 2145
         Width = 1770
         Width = 1500
         Width = 1725
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1905
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 2100
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1725
         Width = 1875
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 2760
         Table = 1455
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 3600
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vw_dossier_elements';






GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 3, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vw_dossier_elements';

