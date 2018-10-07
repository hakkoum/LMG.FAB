﻿CREATE VIEW dbo.vw_prestation_machine_gamme
AS
SELECT     TOP (100) PERCENT dbo.tiers.pk_tiers, dbo.tiers.nom AS nom_tiers, dbo.tiers.adresse1, paramDetail_1.code AS type_tiers, dbo.paramDetail.code AS type_prestation, 
                      dbo.prestation.pk_prestation, dbo.prestation.tauxHoraire, dbo.machine.pk_machine, dbo.machine.nom AS nom_machine, dbo.machine.tournees, dbo.machine.roto, 
                      dbo.machine.coming, dbo.machine.cameron, dbo.machine.fausseCoupe, dbo.machine.sousMultiplesCahiers, paramDetail_2.code AS type_gamme_machine, 
                      dbo.machineGamme.codeDatexp, dbo.machineGamme.actif
FROM         dbo.tiers INNER JOIN
                      dbo.paramDetail AS paramDetail_1 ON dbo.tiers.fk_paramDetail_type = paramDetail_1.pk_paramDetail INNER JOIN
                      dbo.paramDetail INNER JOIN
                      dbo.prestation ON dbo.paramDetail.pk_paramDetail = dbo.prestation.fk_paramDetail_trfo ON dbo.tiers.pk_tiers = dbo.prestation.fk_tiers INNER JOIN
                      dbo.machine ON dbo.prestation.pk_prestation = dbo.machine.fk_prestation INNER JOIN
                      dbo.machineGamme ON dbo.machine.pk_machine = dbo.machineGamme.fk_machine INNER JOIN
                      dbo.paramDetail AS paramDetail_2 ON dbo.machineGamme.fk_paramDetail_gamm = paramDetail_2.pk_paramDetail
ORDER BY nom_tiers

GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[23] 2[7] 3) )"
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
               Top = 49
               Left = 286
               Bottom = 242
               Right = 494
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "paramDetail_1"
            Begin Extent = 
               Top = 76
               Left = 51
               Bottom = 195
               Right = 251
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "paramDetail"
            Begin Extent = 
               Top = 176
               Left = 532
               Bottom = 295
               Right = 732
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "prestation"
            Begin Extent = 
               Top = 32
               Left = 564
               Bottom = 151
               Right = 764
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "machine"
            Begin Extent = 
               Top = 4
               Left = 919
               Bottom = 199
               Right = 1119
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "machineGamme"
            Begin Extent = 
               Top = 14
               Left = 1224
               Bottom = 181
               Right = 1424
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "paramDetail_2"
            Begin Extent = 
               Top = 187
               Left = 1116
               Bottom = 332
               Right = 1316
            End
     ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vw_prestation_machine_gamme';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'       DisplayFlags = 280
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
         Width = 2100
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
         Alias = 900
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vw_prestation_machine_gamme';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vw_prestation_machine_gamme';

