import { EnumStatus } from '../enums/enum-status';

export interface Machine {
  id: string;
  name: string;
  location: string;
  latitude: number | null;
  longitude: number | null;
  status: EnumStatus;
}
