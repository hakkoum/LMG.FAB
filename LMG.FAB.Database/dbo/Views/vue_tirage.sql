CREATE view vue_tirage as
select fk_livre idIntranet,
       codeTirageEdition idTirage
from RefLMG.dbo.livreTirages