import { EnumRole } from '../../../10_Common/Enumerations/EnumRole';

export const MENU_CONFIG: Record<EnumRole, { label: string, route?: string, action?: string }[]> = {

  [EnumRole.Administrator]: [
    { label: 'BackOfficeMenuSection.Home', route: '/backhome' },
    { label: 'BackOfficeMenuSection.Roles', route: '/backhome/roles' },
    { label: 'BackOfficeMenuSection.Users', route: '/backhome/users' },
    { label: 'BackOfficeMenuSection.Disconnect', action: 'logout' }
  ],

  [EnumRole.Inspector]: [
    { label: 'FrontOfficeMenuSection.Home', route: '/fronthome' },
    { label: 'FrontOfficeMenuSection.Disconnect', action: 'logout' }
  ],

  [EnumRole.User]: [
    { label: 'FrontOfficeMenuSection.Special', route: '/fronthome' },
    { label: 'FrontOfficeMenuSection.Disconnect', action: 'logout' }
  ]
};
