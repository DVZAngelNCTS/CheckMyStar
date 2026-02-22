import { Injectable } from '@angular/core';
import { FolderDalService } from '../../70_Dal/BackOffice/Folder-dal.service';
import { FolderGetRequest } from '../../40_Requests/BackOffice/Folder-get.request';
import { FolderDeleteRequest } from '../../40_Requests/BackOffice/Folder-delete.request';
import { FolderModel } from '../../20_Models/BackOffice/Folder.model';

@Injectable({
  providedIn: 'root'
})
export class FolderBllService {
  constructor(private folderDal: FolderDalService) {}

  getNextIdentifier$() {
    return this.folderDal.getNextIdentifier$();
  }

  getFolders$(accommodationName?: string, ownerLastName?: string, inspectorLastName?: string, folderStatus?: number) {
    const request: FolderGetRequest = {
      accommodationName: accommodationName,
      ownerLastName: ownerLastName,
      inspectorLastName: inspectorLastName,
      folderStatus: folderStatus
    };

    return this.folderDal.getFolders$(request);
  }

  getfolder$(identifier: number) {
    const request: FolderGetRequest = {
      folderIdentifier: identifier
    };

    return this.folderDal.getFolder$(request);
  }

  getfoldersByInspector$(inspectorIdentifier: number) {
    const request: FolderGetRequest = {
      inspectorIdentifier: inspectorIdentifier
    };

    return this.folderDal.getFoldersByInspector$(request);
  }

  createFolder$(folder: FolderModel) {
    return this.folderDal.createFolder$(folder);
  }

  updateFolder$(folder: FolderModel) {
    return this.folderDal.updateFolder$(folder);
  }

  deleteFolder$(folderIdentifier: number) {
    const request: FolderDeleteRequest = {
      identifier: folderIdentifier
    };

    return this.folderDal.deleteFolder$(request);
  }
}
