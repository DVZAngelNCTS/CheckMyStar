import { Injectable } from '@angular/core';
import { FolderDalService } from '../../70_Dal/BackOffice/Folder-dal.service';
import { FolderGetRequest } from '../../40_Requests/BackOffice/Folder-get.request';
import { FolderModel } from '../../20_Models/BackOffice/Folder.model';

@Injectable({
  providedIn: 'root'
})
export class FolderBllService {
  constructor(private folderDal: FolderDalService) {}

  getNextIdentifier$() {
    return this.folderDal.getNextIdentifier$();
  }

  getFolders$(accommodationName?: string, ownerLastName?: string, inspectorLastName?: string, folderStatus?: number | null) {
    const request: FolderGetRequest = {
      accommodationName: (accommodationName && accommodationName.trim()) ? accommodationName.trim() : undefined,
      ownerLastName: (ownerLastName && ownerLastName.trim()) ? ownerLastName.trim() : undefined,
      inspectorLastName: (inspectorLastName && inspectorLastName.trim()) ? inspectorLastName.trim() : undefined,
      folderStatus: (folderStatus && folderStatus > 0) ? folderStatus : undefined
    };
    return this.folderDal.getFolders$(request);
  }

  createFolder$(folder: FolderModel) {
    return this.folderDal.createFolder$(folder);
  }

  deleteFolder$(folderIdentifier: number) {
    return this.folderDal.deleteFolder$(folderIdentifier);
  }
}
