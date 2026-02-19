using Microsoft.EntityFrameworkCore;

using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Dal.Results;
using CheckMyStar.Data;
using CheckMyStar.Data.Abstractions;

namespace CheckMyStar.Dal
{
    public class FolderDal(ICheckMyStarDbContext dbContext) : IFolderDal
    {
        public async Task<FolderResult> GetNextIdentifier(CancellationToken ct)
        {
            FolderResult folderResult = new FolderResult();

            try
            {
                var existingIdentifiers = await (from f in dbContext.Folders.AsNoTracking()
                                                 orderby f.Identifier
                                                 select f.Identifier).ToListAsync(ct);

                if (existingIdentifiers.Count == 0)
                {
                    folderResult.IsSuccess = true;
                    folderResult.Folder = new Folder { Identifier = 1 };
                    folderResult.Message = "Identifiant récupéré avec succès";
                }
                else
                {
                    int nextIdentifier = 1;

                    foreach (var id in existingIdentifiers)
                    {
                        if (id == nextIdentifier)
                        {
                            nextIdentifier++;
                        }
                        else if (id > nextIdentifier)
                        {
                            break;
                        }
                    }

                    folderResult.IsSuccess = true;
                    folderResult.Folder = new Folder { Identifier = nextIdentifier };
                    folderResult.Message = "Identifiant récupéré avec succès";
                }
            }
            catch (Exception ex)
            {
                folderResult.IsSuccess = false;
                folderResult.Message = ex.Message;
            }

            return folderResult;
        }

        public async Task<FolderResult> GetFolder(int folderIdentifier, CancellationToken ct)
        {
            FolderResult folderResult = new FolderResult();

            try
            {
                var folder = await (from f in dbContext.Folders.AsNoTracking()
                                   where f.Identifier == folderIdentifier
                                   select f).FirstOrDefaultAsync(ct);

                folderResult.IsSuccess = true;
                folderResult.Folder = folder;
            }
            catch (Exception ex)
            {
                folderResult.IsSuccess = false;
                folderResult.Message = ex.Message;
            }

            return folderResult;
        }

        public async Task<FoldersResult> GetFolders(CancellationToken ct)
        {
            FoldersResult foldersResult = new FoldersResult();

            try
            {
                var folders = await (from f in dbContext.Folders.AsNoTracking()
                                    select f).ToListAsync(ct);

                foldersResult.IsSuccess = true;
                foldersResult.Folders = folders;
                foldersResult.Message = $"{folders.Count} dossier(s) récupéré(s) avec succès";
            }
            catch (Exception ex)
            {
                foldersResult.IsSuccess = false;
                foldersResult.Message = ex.Message;
            }

            return foldersResult;
        }

        public async Task<BaseResult> AddFolder(Folder folder, CancellationToken ct)
        {
            BaseResult baseResult = new BaseResult();

            try
            {
                await dbContext.AddAsync(folder, ct);

                bool result = await dbContext.SaveChangesAsync() > 0 ? true : false;

                if (result)
                {
                    baseResult.IsSuccess = true;
                    baseResult.Message = $"Dossier {folder.Identifier} ajouté avec succès";
                }
                else
                {
                    baseResult.IsSuccess = false;
                    baseResult.Message = $"Impossible d'ajouter le dossier {folder.Identifier}";
                }
            }
            catch (Exception ex)
            {
                baseResult.IsSuccess = false;
                baseResult.Message = $"Impossible d'ajouter le dossier {folder.Identifier} : " + ex.Message;
            }

            return baseResult;
        }

        public async Task<BaseResult> UpdateFolder(Folder folder, CancellationToken ct)
        {
            BaseResult baseResult = new BaseResult();

            try
            {
                await dbContext.UpdateAsync(folder, ct);

                bool result = await dbContext.SaveChangesAsync() > 0 ? true : false;

                if (result)
                {
                    baseResult.IsSuccess = true;
                    baseResult.Message = $"Dossier {folder.Identifier} modifié avec succès";
                }
                else
                {
                    baseResult.IsSuccess = false;
                    baseResult.Message = $"Impossible de modifier le dossier {folder.Identifier}";
                }
            }
            catch (Exception ex)
            {
                baseResult.IsSuccess = false;
                baseResult.Message = $"Impossible de modifier le dossier {folder.Identifier} : " + ex.Message;
            }

            return baseResult;
        }

        public async Task<BaseResult> DeleteFolder(Folder folder, CancellationToken ct)
        {
            BaseResult baseResult = new BaseResult();

            try
            {
                await dbContext.RemoveAsync(folder, ct);

                bool result = await dbContext.SaveChangesAsync() > 0 ? true : false;

                if (result)
                {
                    baseResult.IsSuccess = true;
                    baseResult.Message = $"Dossier {folder.Identifier} supprimé avec succès";
                }
                else
                {
                    baseResult.IsSuccess = false;
                    baseResult.Message = $"Impossible de supprimer le dossier {folder.Identifier}";
                }
            }
            catch (Exception ex)
            {
                baseResult.IsSuccess = false;
                baseResult.Message = $"Impossible de supprimer le dossier {folder.Identifier} : " + ex.Message;
            }

            return baseResult;
        }
    }
}
