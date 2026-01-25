import { EnumRole } from '../../../10_Common/Enumerations/EnumRole';

export const MENU_CONFIG: Record<EnumRole, { icon: string, label: string, route?: string, action?: string }[]> = {

  [EnumRole.Administrator]: [
    { icon: 'bi bi-house', label: 'BackOfficeMenuSection.Home', route: '/backhome' },
    { icon: 'bi bi-shield-check', label: 'BackOfficeMenuSection.Roles', route: '/backhome/roles' },
    { icon: 'bi bi-people', label: 'BackOfficeMenuSection.Users', route: '/backhome/users' },
    { icon: 'bi bi-door-closed', label: 'BackOfficeMenuSection.Disconnect', action: 'logout' }
  ],

  [EnumRole.Inspector]: [
    { icon: 'bi bi-house', label: 'FrontOfficeMenuSection.Home', route: '/fronthome' },
    { icon: 'bi bi-door-closed', label: 'FrontOfficeMenuSection.Disconnect', action: 'logout' }
  ],

  [EnumRole.User]: [
    { icon: 'bi bi-house', label: 'FrontOfficeMenuSection.Special', route: '/fronthome' },
    { icon: 'bi bi-door-closed', label: 'FrontOfficeMenuSection.Disconnect', action: 'logout' }
  ]
};
