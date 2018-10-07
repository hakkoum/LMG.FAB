CREATE VIEW dbo.vw_tiers_prestations
AS
SELECT     TOP (100) PERCENT dbo.tiers.pk_tiers, dbo.tiers.fk_paramDetail_type, dbo.tiers.nom, dbo.tiers.adresse1, dbo.tiers.adresse2, dbo.tiers.adresse3, 
                      dbo.tiers.codePostal, dbo.tiers.ville, dbo.tiers.pays, dbo.tiers.nomContact, dbo.tiers.telephone, dbo.tiers.codeFournisseurERP, dbo.tiers.codeEntrepotERP, 
                      dbo.tiers.signatureFicheTechnique, dbo.tiers.texteCommande, dbo.tiers.actif, dbo.tiers.adresseEmail, paramDetail_1.code AS type_tiers, 
                      dbo.paramDetail.code AS type_prestation, dbo.prestation.tauxHoraire
FROM         dbo.tiers INNER JOIN
                      dbo.paramDetail AS paramDetail_1 ON dbo.tiers.fk_paramDetail_type = paramDetail_1.pk_paramDetail LEFT OUTER JOIN
                      dbo.paramDetail INNER JOIN
                      dbo.prestation ON dbo.paramDetail.pk_paramDetail = dbo.prestation.fk_paramDetail_trfo ON dbo.tiers.pk_tiers = dbo.prestation.fk_tiers
ORDER BY dbo.tiers.nom

GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[14] 2[8] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
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
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "tiers"
            Begin Extent = 
               Top = 5
               Left = 346
               Bottom = 330
               Right = 554
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "prestation"
            Begin Extent = 
               Top = 73
               Left = 638
               Bottom = 263
               Right = 838
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "paramDetail_1"
            Begin Extent = 
               Top = 102
               Left = 33
               Bottom = 332
               Right = 233
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "paramDetail"
            Begin Extent = 
               Top = 59
               Left = 889
               Bottom = 239
               Right = 1089
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 21
         Width = 284
         Width = 1500
         Width = 1095
         Width = 1695
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vw_tiers_prestations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'     Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vw_tiers_prestations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vw_tiers_prestations';

