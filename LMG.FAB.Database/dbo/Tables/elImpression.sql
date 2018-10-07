CREATE TABLE [dbo].[elImpression] (
    [pk_elImpression]           INT NOT NULL,
    [fk_machine]                INT NULL,
    [fk_paramDetail_pell]       INT NULL,
    [fk_paramDetail_vern]       INT NULL,
    [fk_paramDetail_coul_recto] INT NULL,
    [fk_paramDetail_coul_verso] INT NULL,
    [fk_paramdetail_type]       INT NULL,
    PRIMARY KEY CLUSTERED ([pk_elImpression] ASC)
);

