CREATE VIEW dbo.vw_articles
AS
SELECT     dbo.article.pk_article, dbo.article.idIntranet, dbo.article.titre, dbo.article.hauteur, dbo.article.largeur, dbo.article.nbPages, dbo.article.nbSignesCalibres, 
                      dbo.article.nbSignesEvalues, dbo.article.observation, dbo.article.nbPagesHorsTexte, dbo.article.nbPagesHorsTexteDef, dbo.article.nbPagesInterieurDef, 
                      paramDetail_1.code AS type_pres, paramDetail_2.code AS type_form, paramDetail_3.code AS type_tyac, dbo.paramDetail.code AS type_gamm
FROM         dbo.article LEFT OUTER JOIN
                      dbo.paramDetail ON dbo.article.fk_paramDetail_gamm = dbo.paramDetail.pk_paramDetail LEFT OUTER JOIN
                      dbo.paramDetail AS paramDetail_3 ON dbo.article.fk_paramDetail_tyac = paramDetail_3.pk_paramDetail LEFT OUTER JOIN
                      dbo.paramDetail AS paramDetail_2 ON dbo.article.fk_paramDetail_form = paramDetail_2.pk_paramDetail LEFT OUTER JOIN
                      dbo.paramDetail AS paramDetail_1 ON dbo.article.fk_paramDetail_pres = paramDetail_1.pk_paramDetail

GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[43] 4[27] 2[6] 3) )"
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
         Begin Table = "article"
            Begin Extent = 
               Top = 22
               Left = 290
               Bottom = 330
               Right = 490
            End
            DisplayFlags = 280
            TopColumn = 8
         End
         Begin Table = "paramDetail"
            Begin Extent = 
               Top = 163
               Left = 911
               Bottom = 282
               Right = 1111
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "paramDetail_3"
            Begin Extent = 
               Top = 49
               Left = 858
               Bottom = 168
               Right = 1058
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "paramDetail_2"
            Begin Extent = 
               Top = 100
               Left = 58
               Bottom = 219
               Right = 258
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "paramDetail_1"
            Begin Extent = 
               Top = 48
               Left = 561
               Bottom = 155
               Right = 761
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
      Begin ColumnWidths = 20
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
         Wid', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vw_articles';






GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'th = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 2025
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1800
         Alias = 2475
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vw_articles';






GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vw_articles';

