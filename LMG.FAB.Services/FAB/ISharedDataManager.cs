using LMG.Fab.Data.Entities.LmgFab;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMG.FAB.Services.FAB
{
    public interface ISharedDataManager
    {

        /// <summary>
        /// Renvoi la liste des valeurs d'un parametre à partir de son code
        /// </summary>
        /// <param name="codeParam">code du parametre à retourner</param>
        /// <returns></returns>
        List<ParamDetail> GetParamDetail(string codeParam);

        /// <summary>
        /// renvoi la liste de tous les parametres avec leur valeurs
        /// </summary>
        /// <returns></returns>
        List<ParamDetail> GetParamDetail();

        /// <summary>
        /// renvoi la liste de paramTable avec leur detail
        /// </summary>
        /// <returns></returns>
        List<ParamTable> GetParamTable();

        /// <summary>
        /// retourne la liste des champs de chaque table 
        /// </summary>
        /// <returns></returns>
        Dictionary<string,List<string>> GetDbFields();

        /// <summary>
        /// Sauvegarde un paramTable (Ajout ou update)
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        ParamTable SaveParamTable(ParamTable param);

        /// <summary>
        /// Sauvegarde un ParamDetail (Ajout ou update)
        /// </summary>
        /// <param name="detail"></param>
        /// <returns></returns>
        ParamDetail SaveParamDetail(ParamDetail detail);

        /// <summary>
        /// supprimer un param table, cela supprime aussi tous les parametres details. Si le parametre est referencé dans une table, la suppression va genérer une erreur  
        /// </summary>
        /// <param name="paramId"></param>
        void DeleteParamTable(int paramId);

        /// <summary>
        /// supprimer un param detail. Si le parametre est referencé dans une table, la suppression va genérer une erreur  
        /// </summary>
        /// <param name="paramId"></param>
        void DeleteParamDetail(int paramDetailId);

        /// <summary>
        /// retourne le nom + prenom de l'utilisateur connecté depuis son login
        /// </summary>
        /// <returns></returns>
        string GetUserName();

        Commentaire AddComment(string tableName, int recordKey, string fieldName, string comment, int isImportant);

        void UpdateComment(int commentId, string comment, int isImportant);

        void DeleteComment(int commentId);

        List<Commentaire> GetRecordComments(string tableName, int recordKey);
        Commentaire GetComment(int commentId);

        /// <summary>
        /// Recuperer la liste des collections technique
        /// </summary>
        /// <returns></returns>
        List<CollectionTechnique> GetCollectionTechnique();

    }
}
