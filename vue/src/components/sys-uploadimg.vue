<template>
    <div>
      <div>
        <template v-for="item in thisuploadList">
        <div class="demo-upload-list" v-if="item.status === 'finished'">
            <img :src="getUrl(item.url)">
            <div class="demo-upload-list-cover">
                <Icon type="ios-eye-outline" @click.native="handleView(item.url)"></Icon>
                <Icon type="ios-trash-outline" @click.native="handleRemove(item)"></Icon>
            </div>
        </div>
        <div class="demo-upload-list" v-else-if="item.status != 'delete'">
            <Progress v-if="item.showProgress" :percent="item.percentage" hide-info></Progress>
        </div>
        </template>
    <Upload
        ref="upload"
        :show-upload-list="false"
        :default-file-list="defaultList"
        :on-success="handleSuccess"
        :format="['jpg','jpeg','png']"
        :max-size="2048"
        :on-format-error="handleFormatError"
        :on-exceeded-size="handleMaxSize"
        :before-upload="handleBeforeUpload"
        multiple
        type="drag"
        :action="getUrl('/api/Upload/UploadImage')"
        style="display: inline-block;width:58px;">
        <div style="width: 58px;height:58px;line-height: 58px;">
            <Icon type="ios-camera" size="20"></Icon>
        </div>
    </Upload>
    </div>
    <Modal title="图片预览" v-model="visible">
        <img :src="modalimgUrl" v-if="visible" style="width: 100%">
    </Modal>
    </div>
</template>
<script lang="ts">
    import { Component, Vue, Inject, Prop, Watch } from "vue-property-decorator"
    import Util from '../lib/util'
    import AbpBase from '../lib/abpbase'
    import appconst from '../lib/appconst'
@Component({})
export default class SysUploadImg  extends AbpBase{
  @Prop({type:Number,default:5}) imgNum:number
  @Prop({type:Array,default:()=>[]}) uploadList:Array<any>
  thisuploadList:Array<any>=[]
  defaultList: Array<any>=[]
  modalimgUrl: string = ""
  visible: boolean = false
  handleView(url) {
    this.modalimgUrl = this.getUrl(url)
    this.visible = true
  }
  handleRemove(file) {
     const fileList = this.$refs.upload.fileList
     this.$refs.upload.fileList.splice(fileList.indexOf(file), 1)
      this.thisuploadList.forEach((item)=>{
        if(item.url==file.url){
          item.status = "delete"
        }
      })
      this.$emit('save-success',[])
  }
  handleSuccess(res, file) {
    file.url = res.result[0].url
    file.name = res.result[0].fileName
    file.status = "finished"
    this.$emit('save-success',file)
  }
  handleFormatError(file) {
    this.$Modal.warning({
        title: '文件格式不正确，请选择jpg或png格式'
    });
  }
  handleMaxSize(file) {
    this.$Modal.warning({
        title: '超出文件大小限制2M'
    });
  }
  handleBeforeUpload() {
    if(this.imgNum == 1){
      this.thisuploadList.forEach((item)=>{
        item.status = "delete"
      })
      return true
    }
    let maxlength = this.imgNum+1
    const check = this.thisuploadList.length < maxlength
    if (!check) {
      this.$Modal.warning({
          title: `最多可上传${this.imgNum}张图片`
      });
    }
    return check;
  }
  mounted() {
    this.thisuploadList = this.$refs.upload.fileList
  }
  @Watch('uploadList')
  langChange(){
    this.thisuploadList = this.uploadList
  }
}
</script>
<style scoped>
.demo-upload-list {
  display: inline-block;
  width: 60px;
  height: 60px;
  text-align: center;
  line-height: 60px;
  border: 1px solid transparent;
  border-radius: 4px;
  overflow: hidden;
  background: #fff;
  position: relative;
  box-shadow: 0 1px 1px rgba(0, 0, 0, 0.2);
  margin-right: 4px;
}
.demo-upload-list img {
  width: 100%;
  height: 100%;
}
.demo-upload-list-cover {
  display: none;
  position: absolute;
  top: 0;
  bottom: 0;
  left: 0;
  right: 0;
  background: rgba(0, 0, 0, 0.6);
}
.demo-upload-list:hover .demo-upload-list-cover {
  display: block;
}
.demo-upload-list-cover i {
  color: #fff;
  font-size: 20px;
  cursor: pointer;
  margin: 0 2px;
}
</style>
