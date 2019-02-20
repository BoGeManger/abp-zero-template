import Entity from './entity'
export default class PdBrand extends Entity<number>{
    brandName:string;
    logo:string;
    remark:string;
    isActive:boolean;
}