import { FolderModel } from '../../20_Models/BackOffice/Folder.model';
import { BaseResponse } from '../BaseResponse';

export interface FoldersResponse extends BaseResponse {
  folders?: FolderModel[];
}
