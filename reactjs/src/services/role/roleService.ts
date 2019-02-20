import { CreateRoleInput } from './dto/createRoleInput';
import { CreateRoleOutput } from './dto/createRoleOutput';
import GetRoleAsyncOutput from './dto/getRoleAsyncOutput';
import { UpdateRoleInput } from './dto/updateRoleInput';
import { UpdateRoleOutput } from './dto/updateRoleOutput';
import { EntityDto } from '../dto/entityDto';
import http from '../httpService';
import { GetAllRoleOutput } from './dto/getAllRoleOutput';
import { PagedResultDto } from '../dto/pagedResultDto';
import { GetRoleForEditOutput } from './dto/getRoleForEditOutput';
import { PagedRoleResultRequestDto } from './dto/PagedRoleResultRequestDto';

class RoleService {
  public async create(createRoleInput: CreateRoleInput): Promise<PagedResultDto<CreateRoleOutput>> {
    let result = await http.post('api/services/app/Role/Create', createRoleInput);
    return result.data.result;
  }

  public async getRolesAsync(getRoleAsyncInput: GetRoleAsyncInput): Promise<GetRoleAsyncOutput> {
    let result = await http.get('api/services/app/Role/GetRolesAsync', { params: getRoleAsyncInput });
    return result.data.result;
  }

  public async update(updateRoleInput: UpdateRoleInput): Promise<UpdateRoleOutput> {
    let result = await http.put('api/services/app/Role/Update', updateRoleInput);
    return result.data.result as UpdateRoleOutput;
  }

  public async delete(entityDto: EntityDto) {
    let result = await http.delete('api/services/app/Role/Delete', { params: entityDto });
    return result.data;
  }

  public async getAllPermissions() {
    let result = await http.get('api/services/app/Role/GetAllPermissions');
    return result.data.result.items;
  }

  public async getRoleForEdit(entityDto: EntityDto): Promise<GetRoleForEditOutput> {
    let result = await http.get('api/services/app/Role/GetRoleForEdit', { params: entityDto });
    return result.data.result;
  }

  public async get(entityDto: EntityDto) {
    let result = await http.get('api/services/app/Role/Get', { params: entityDto });
    return result.data;
  }

  public async getAll(pagedFilterAndSortedRequest: PagedRoleResultRequestDto): Promise<PagedResultDto<GetAllRoleOutput>> {
    let result = await http.get('api/services/app/Role/GetAll', { params: pagedFilterAndSortedRequest });
    return result.data.result;
  }
}

export default new RoleService();
