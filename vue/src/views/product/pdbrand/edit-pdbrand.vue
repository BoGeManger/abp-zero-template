<template>
    <div>
        <Modal
         title="编辑品牌"
         :value="value"
         @on-ok="save"
         @on-visible-change="visibleChange"
        >
            <Form ref="pdbrandForm"  label-position="top" :rules="pdbrandRule" :model="pdbrand">
                <FormItem label="品牌名称" prop="brandName">
                            <Input v-model="pdbrand.brandName" :maxlength="64"></Input>
                        </FormItem>
                        <FormItem label="备注" prop="remark">
                            <Input v-model="pdbrand.remark" :maxlength="200"></Input>
                        </FormItem>
                        <FormItem >
                         <sys-Uploadimg  ref="uploadimg" :imgNum="1" :uploadList="uploadList" @save-success="setLogo"></sys-Uploadimg> 
                        </FormItem>
                        <FormItem>
                    <Checkbox v-model="pdbrand.isActive">{{L('IsActive')}}</Checkbox>
                </FormItem>

            </Form>
            <div slot="footer">
                <Button @click="cancel">{{L('Cancel')}}</Button>
                <Button @click="save" type="primary">{{L('OK')}}</Button>
            </div>
        </Modal>
    </div>
</template>
<script lang="ts">
import { Component, Vue, Inject, Prop, Watch } from "vue-property-decorator";
import Util from "../../../lib/util";
import AbpBase from "../../../lib/abpbase";
import PdBrand from "@/store/entities/pdbrand";
import SysUploadimg from "../../../components/sys-uploadimg.vue";
@Component({
  components: { SysUploadimg }
})
export default class EditPdBrand extends AbpBase {
  @Prop({ type: Boolean, default: false })
  value: boolean;
  pdbrand: PdBrand = new PdBrand()
  get permissions() {
    return this.$store.state.pdbrand.permissions
  }
  get uploadList(){
    if(this.pdbrand.logo==null){
      return []
    }
     return [{url:this.pdbrand.logo,status:'finished'}]
  }
  save() {
    (this.$refs.pdbrandForm as any).validate(async (valid: boolean) => {
      if (valid) {
        await this.$store.dispatch({
          type: "pdbrand/update",
          data: this.pdbrand
        });
        (this.$refs.pdbrandForm as any).resetFields();
        this.$emit("save-success");
        this.$emit("input", false);
      }
    })
  }
  cancel() {
    (this.$refs.pdbrandForm as any).resetFields();
    this.$emit("input", false);
  }
  visibleChange(value: boolean) {
    if (!value) {
      this.$emit("input", value)
    } else {
      this.pdbrand = Util.extend(
        true,
        {},
        this.$store.state.pdbrand.editPdBrand
      );
    }
  }
  setLogo(imgList) {
    this.pdbrand.logo = imgList.url
  }
  pdbrandRule = {
    brandName: [
      {
        required: true,
        message: this.L("必填", undefined, "品牌名称"),
        trigger: "blur"
      }
    ]
  };
}
</script>

