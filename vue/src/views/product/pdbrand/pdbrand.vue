<template>
    <div>
        <Card dis-hover>
            <div class="page-body">
                <Form ref="queryForm" :label-width="80" label-position="left" inline>
                    <Row :gutter="16">
                        <Col span="6">
                            <FormItem :label="'品牌名称'" style="width:100%">
                                <Input v-model="pagerequest.brandName"></Input>
                            </FormItem>
                        </Col>
                        <Col span="6">
                            <FormItem :label="L('IsActive')+':'" style="width:100%">
                                <Select :placeholder="L('Select')" @on-change="isActiveChange">
                                    <Option value="All">{{L('All')}}</Option>
                                    <Option value="Actived">{{L('Actived')}}</Option>
                                    <Option value="NoActive">{{L('NoActive')}}</Option>
                                </Select>
                            </FormItem>
                        </Col>
                        <Col span="6">
                            <FormItem :label="L('CreationTime')+':'" style="width:100%">
                                <DatePicker  v-model="creationTime" type="datetimerange" format="yyyy-MM-dd" style="width:100%" placement="bottom-end" :placeholder="L('SelectDate')"></DatePicker>
                            </FormItem>
                        </Col>
                    </Row>
                    <Row>
                        <Button @click="create" icon="android-add" type="primary" size="large">{{L('Add')}}</Button>
                        <Button icon="ios-search" type="primary" size="large" @click="getpage" class="toolbar-btn">{{L('Find')}}</Button>
                    </Row>
                </Form>
                <div class="margin-top-10">
                    <Table :loading="loading" :columns="columns" :no-data-text="L('NoDatas')" border :data="list">
                    </Table>
                    <Page  show-sizer class-name="fengpage" :total="totalCount" class="margin-top-10" @on-change="pageChange" @on-page-size-change="pagesizeChange" :page-size="pageSize" :current="currentPage"></Page>
                </div>
            </div>
        </Card>
        <create-pdbrand v-model="createModalShow" @save-success="getpage"></create-pdbrand>
        <edit-pdbrand v-model="editModalShow" @save-success="getpage"></edit-pdbrand> 
    </div>
</template>
<script lang="ts">
import { Component, Vue, Inject, Prop, Watch } from "vue-property-decorator";
import Util from "../../../lib/util";
import AbpBase from "../../../lib/abpbase";
import { FieldType, Filter, CompareType } from "../../../store/entities/filter";
import PageRequest from "../../../store/entities/page-request";
import CreatePdbrand from "./create-pdbrand.vue";
import EditPdbrand from "./edit-pdbrand.vue";
class PagePdBrandRequest extends PageRequest {
  userName: string;
  name: string;
  isActive: boolean = null; //nullable
  from: Date;
  to: Date;
}

@Component({
  components: { CreatePdbrand, EditPdbrand }
})
export default class PdBrands extends AbpBase {
  edit() {
    this.editModalShow = true;
  }
  //filters
  pagerequest: PagePdBrandRequest = new PagePdBrandRequest();
  creationTime: Date[] = [];

  createModalShow: boolean = false;
  editModalShow: boolean = false;
  get list() {
    return this.$store.state.pdbrand.list;
  }
  get loading() {
    return this.$store.state.pdbrand.loading;
  }
  create() {
    this.createModalShow = true;
  }
  isActiveChange(val: string) {
    console.log(val);
    if (val === "Actived") {
      this.pagerequest.isActive = true;
    } else if (val === "NoActive") {
      this.pagerequest.isActive = false;
    } else {
      this.pagerequest.isActive = null;
    }
  }
  pageChange(page: number) {
    this.$store.commit("pdbrand/setCurrentPage", page);
    this.getpage();
  }
  pagesizeChange(pagesize: number) {
    this.$store.commit("pdbrand/setPageSize", pagesize);
    this.getpage();
  }
  async getpage() {
    this.pagerequest.maxResultCount = this.pageSize;
    this.pagerequest.skipCount = (this.currentPage - 1) * this.pageSize;
    //filters

    if (this.creationTime.length > 0) {
      this.pagerequest.from = this.creationTime[0];
    }
    if (this.creationTime.length > 1) {
      this.pagerequest.to = this.creationTime[1];
    }

    await this.$store.dispatch({
      type: "pdbrand/getAll",
      data: this.pagerequest
    });
  }
  get pageSize() {
    return this.$store.state.pdbrand.pageSize;
  }
  get totalCount() {
    return this.$store.state.pdbrand.totalCount;
  }
  get currentPage() {
    return this.$store.state.pdbrand.currentPage;
  }
  columns = [
    {
      title: "品牌名称",
      key: "brandName"
    },
    {
      title: "图标",
      key: "logo",
      align: "center",
      render: (h: any, params: any) => {
        return h("img", {
          attrs: {
            src: this.getUrl(params.row.logo),
            style: "width: 70px;border-radius: 2px;"
          }
        });
      }
    },
    {
      title: "备注",
      key: "remark"
    },
    {
      title: this.L("IsActive"),
      render: (h: any, params: any) => {
        return h("span", params.row.isActive ? this.L("Yes") : this.L("No"));
      }
    },
    {
      title: "创建时间",
      key: "creationTime",
      render: (h: any, params: any) => {
        return h(
          "span",
          new Date(params.row.creationTime).toLocaleDateString()
        );
      }
    },
    {
      title: "修改时间",
      key: "lastModificationTime",
      render: (h: any, params: any) => {
        return h(
          "span",
          params.row.lastModificationTime==null?null:new Date(params.row.lastModificationTime).toLocaleDateString()
        );
      }
    },
    {
      title: this.L("Actions"),
      key: "Actions",
      width: 150,
      render: (h: any, params: any) => {
        return h("div", [
          h(
            "Button",
            {
              props: {
                type: "primary",
                size: "small"
              },
              style: {
                marginRight: "5px"
              },
              on: {
                click: () => {
                  this.$store.commit("pdbrand/edit", params.row);
                  this.edit();
                }
              }
            },
            this.L("Edit")
          ),
          h(
            "Button",
            {
              props: {
                type: "error",
                size: "small"
              },
              on: {
                click: async () => {
                  this.$Modal.confirm({
                    title: this.L("Tips"),
                    content: "确认删除该品牌",
                    okText: this.L("Yes"),
                    cancelText: this.L("No"),
                    onOk: async () => {
                      await this.$store.dispatch({
                        type: "pdbrand/delete",
                        data: params.row
                      });
                      await this.getpage();
                    }
                  });
                }
              }
            },
            this.L("Delete")
          )
        ]);
      }
    }
  ];
  async created() {
    this.getpage();
  }
}
</script>