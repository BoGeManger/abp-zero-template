export interface UpdateRoleInput {
  name: string;
  displayName: string;
  normalizedName: string;
  description: string;
  isStatic: boolean;
  permissions: string[];
  id: number;
}
