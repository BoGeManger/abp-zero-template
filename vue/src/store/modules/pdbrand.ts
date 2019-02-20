import {Store,Module,ActionContext} from 'vuex'
import ListModule from './list-module'
import ListState from './list-state'
import PdBrand from '../entities/pdbrand'
import Ajax from '../../lib/ajax'
import PageResult from '@/store/entities/page-result';

interface PdBrandState extends ListState<PdBrand>{
    editPdBrand:PdBrand;
    permissions:Array<string>
}
class PdBrandModule extends ListModule<PdBrandState,any,PdBrand>{
    state={
        totalCount:0,
        currentPage:1,
        pageSize:10,
        list: new Array<PdBrand>(),
        loading:false,
        editPdBrand:new PdBrand(),
        permissions:new Array<string>()
    }
    actions={
        async getAll(context:ActionContext<PdBrandState,any>,payload:any){
            context.state.loading=true;
            let reponse=await Ajax.get('/api/services/app/PdBrand/GetPaged',{params:payload.data});
            context.state.loading=false;
            let page=reponse.data.result as PageResult<PdBrand>;
            context.state.totalCount=page.totalCount;
            context.state.list=page.items;
        },
        async create(context:ActionContext<PdBrandState,any>,payload:any){
            await Ajax.post('/api/services/app/PdBrand/CreateOrUpdate',{'PdBrand':payload.data});
        },
        async update(context:ActionContext<PdBrandState,any>,payload:any){
            await Ajax.post('/api/services/app/PdBrand/CreateOrUpdate',{'PdBrand':payload.data});
        },
        async delete(context:ActionContext<PdBrandState,any>,payload:any){
            await Ajax.delete('/api/services/app/PdBrand/Delete?Id='+payload.data.id);
        },
        async get(context:ActionContext<PdBrandState,any>,payload:any){
            let reponse=await Ajax.get('/api/services/app/PdBrand/GetById?Id='+payload.id);
            return reponse.data.result as PdBrand;
        }
    };
    mutations={
        setCurrentPage(state:PdBrandState,page:number){
            state.currentPage=page;
        },
        setPageSize(state:PdBrandState,pagesize:number){
            state.pageSize=pagesize;
        },
        edit(state:PdBrandState,pdbrand:PdBrand){
            state.editPdBrand=pdbrand;
        }
    }
}
const pdbrandModule=new PdBrandModule();
export default pdbrandModule;