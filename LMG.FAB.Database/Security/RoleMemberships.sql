EXECUTE sp_addrolemember @rolename = N'db_owner', @membername = N'lmg_fab';


GO
EXECUTE sp_addrolemember @rolename = N'db_datareader', @membername = N'LMG_SSRS_ReadOnly';

