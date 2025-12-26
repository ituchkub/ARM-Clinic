<script setup>
import { onMounted, ref } from "vue";
import { useRouter } from "vue-router";
import _apiDocList from "../../api/document-list";
import { getTokenStorage } from "../../helpers/storage";
import _apiDDL from "../../api/ddl";
import { onSetPagination } from "../../helpers/function-service";
import { useStore } from "vuex";
import VPagination from "@hennge/vue3-pagination";
import "@hennge/vue3-pagination/dist/vue3-pagination.css";
import dayjs from "dayjs";
import { saveExcel } from "@progress/kendo-vue-excel-export";
const $router = useRouter();
const $store = useStore();
const rows = ref([]);
const rowsExport = ref([]);
const formFilter = ref({
  ProjectID: "",
  PMType: "",
  PMName: "",
  PMStatus: "",
  PMFirstname: "",
  PMLastname: "",
  PMInitialName: "",
  PMEmail: "",
  EmpStatus: "",
  PMStartDate: "",
  PMEndDate: "",
});
const DDL = ref({
  status: [],
});
const paginationConfig = ref({
  rowsPerPage: 10,
  pageIndexNumber: 1,
  totalRows: 0,
  totalPages: 0,
});
onMounted(async () => {
  _apiDDL.DDL_Status({}, (response) => {
    DDL.value.status = response.body;
  });
  onLoadData();
});
const onChangPage = (path) => {
  $router.push(path);
};
const onLoadData = async () => {
  $store.commit("setStatusLoading", true);
  // paginationConfig.value.pageIndexNumber = 0
  console.log(
    "getTokenStorage().sestionInfo.Position",
    getTokenStorage().sestionInfo.Position
  );
  const body = {
    PageSize: paginationConfig.value.rowsPerPage,
    PageNo: paginationConfig.value.pageIndexNumber,
    CompanyCode:
      getTokenStorage().sestionInfo.PMRole == "IT"
        ? ""
        : getTokenStorage().sestionInfo.CompanyCode,
    ...formFilter.value,
    PMStatus: formFilter.value?.PMStatus.toString(),
    // EmpStatus: formFilter.value?.EmpStatus.toString()
  };
  await _apiDocList.getdoclog(body, (response) => {
    console.log("response : ", response);
    if (response.body.length > 0) {
      paginationConfig.value.totalRows = response.body[0].totalrow;
      paginationConfig.value.totalPages = Math.ceil(
        paginationConfig.value.totalRows / paginationConfig.value.rowsPerPage
      );
      rows.value = response.body;
      rowsExport.value = response.body;
    } else {
      paginationConfig.value.totalRows = 0;
      paginationConfig.value.totalPages = 0;
      rows.value = [];
    }
  });
  $store.commit("setStatusLoading", false);
};
const onFilter = () => {
  paginationConfig.value.pageIndexNumber = 1;
  onLoadData();
};
const formatDate = (date) => {
  return dayjs(date).format("DD/MM/YYYY HH:mm:ss");
};
const format = (date) => {
  const day = date.getDate();
  const month = date.getMonth() + 1;
  const year = date.getFullYear();
  return `${day}/${month}/${year}`;
};
const onClearForm = () => {
  formFilter.value = {
    ProjectID: "",
    PMName: "",
    PMStatus: "",
    Firstname: "",
    Lastname: "",
    PMInitialName: "",
    EmpStatus: "",
  };
  paginationConfig.value.pageIndexNumber = 1;
  onLoadData();
};
const  kendoExcelExport = async() => {
  const body = {
    PageSize: 5000,
    PageNo: 1,
    CompanyCode:
      getTokenStorage().sestionInfo.PMRole == "IT"
        ? ""
        : getTokenStorage().sestionInfo.CompanyCode,
    ...formFilter.value,
    PMStatus: formFilter.value?.PMStatus.toString(),
    // EmpStatus: formFilter.value?.EmpStatus.toString()
  }
  await _apiDocList.getdoclog(body, (response) => {
    console.log("response : ", response);
    if (response.body.length > 0) {
      rowsExport.value = response.body;
    }
  })
  const modifiedData = rowsExport.value.map((row) => {
    if (row.PMType == "P") {
      row.PMTypeN = "โครงการ";
    } else {
      row.PMTypeN = "งบการเงิน";
    }
    //   row.Firstname = row.Firstname + ' ' + row.Lastname
    if (row.EmpStatus == true) {
      row.EmpStatusN = "Employee Accepted";
    } else {
      row.EmpStatusN = "Waiting Employee Accept";
    }
    if (row?.UpdateDateTime != null)
      row.UpdateDateTimeN = formatDate(row?.UpdateDateTime);
    return row;
  });
  console.log("rows.value", rows.value);
  saveExcel({
    data: modifiedData,
    fileName: "Blackout list Report.xlsx",
    columns: [
      { field: "ProjectID", title: "Project ID" },
      { field: "PMTypeN", title: "Project Category" },
      { field: "PMName", title: "Project Name" },
      { field: "Title", title: "Title" },
      { field: "Firstname", title: "Firstname" },
      { field: "Lastname", title: "Lastname" },
      { field: "PMInitialName", title: "PM Name" },
      { field: "Position", title: "Position" },
      { field: "PMDepartment", title: "PM Department" },
      { field: "Companyname", title: "Company Name" },
      { field: "StatusDesc", title: "Status" },
      { field: "EmpStatusN", title: "EmpStatus" },
      { field: "UpdateDateTimeN", title: "Status Date" },
    ],
  });
};
</script>
<template>
  <div class="px-6">
    <!-- {{ DDL.status }} -->
    <div class="flex mt-8 justify-between items-center">
      <span class="text-3xl font-bold">All List Report</span>
      <div class="flex space-x-6">
        <button
          @click="onChangPage('/add-project')"
          class="rounded-full px-8 py-3 bg-custom-blue hover:bg-amber-600t"
        >
          <div class="flex space-x-2">
            <svg
              width="25"
              height="25"
              viewBox="0 0 25 25"
              fill="none"
              xmlns="http://www.w3.org/2000/svg"
            >
              <mask
                id="mask0_27_767"
                style="mask-type: alpha"
                maskUnits="userSpaceOnUse"
                x="0"
                y="0"
                width="25"
                height="25"
              >
                <rect x="0.5" y="0.5" width="24" height="24" fill="#D9D9D9" />
              </mask>
              <g mask="url(#mask0_27_767)">
                <path
                  d="M11.5 13.5H5.5V11.5H11.5V5.5H13.5V11.5H19.5V13.5H13.5V19.5H11.5V13.5Z"
                  fill="white"
                />
              </g>
            </svg>
            <span class="text-white text-lgtext-lg font-bold">Add Project</span>
          </div>
        </button>
      </div>
    </div>
    <div class="mt-8 flex space-x-2" style="margin-bottom: -10px">
      <button class="px-9 py-3 rounded-t-xl bg-teal-500 text-gray-600">
        <div class="flex space-x-2 items-center">
          <!-- <div class="px-2 py-1 bg-white rounded-2xl w-14">
                        <span class="text-green-700">20</span>
                    </div> -->
          <span class="text-lg font-bold text-white">All Blackout Data</span>
        </div>
      </button>
    </div>
    <div class="absolute left-0 right-0 px-6">
      <div class="rounded-xl bg-white px-6 py-10">
        <div class="flex justify-between">
          <div class="flex space-x-3 items-center">
            <svg
              width="33"
              height="33"
              viewBox="0 0 33 33"
              fill="none"
              xmlns="http://www.w3.org/2000/svg"
            >
              <mask
                id="mask0_246_12511"
                style="mask-type: alpha"
                maskUnits="userSpaceOnUse"
                x="0"
                y="0"
                width="33"
                height="33"
              >
                <rect x="0.5" y="0.5" width="32" height="32" fill="#D9D9D9" />
              </mask>
              <g mask="url(#mask0_246_12511)">
                <path
                  d="M4.5 24.5V21.8333H12.5V24.5H4.5ZM4.5 17.8333V15.1667H20.5V17.8333H4.5ZM4.5 11.1667V8.5H28.5V11.1667H4.5Z"
                  fill="#48A23F"
                />
              </g>
            </svg>
            <span class="text-lg font-bold">Filter</span>
          </div>
          <button @click="onClearForm">
            <div class="flex space-x-3 items-center">
              <svg
                width="32"
                height="32"
                viewBox="0 0 32 32"
                fill="none"
                xmlns="http://www.w3.org/2000/svg"
              >
                <mask
                  id="mask0_168_48011"
                  style="mask-type: alpha"
                  maskUnits="userSpaceOnUse"
                  x="0"
                  y="0"
                  width="32"
                  height="32"
                >
                  <rect width="32" height="32" fill="#D9D9D9" />
                </mask>
                <g mask="url(#mask0_168_48011)">
                  <path
                    d="M15.974 18C15.4287 18 14.9591 17.803 14.5651 17.409C14.1711 17.015 13.974 16.5453 13.974 16C13.974 15.4548 14.1711 14.9851 14.5651 14.5911C14.9591 14.1971 15.4287 14.0001 15.974 14.0001C16.5193 14.0001 16.9889 14.1971 17.3829 14.5911C17.7769 14.9851 17.9739 15.4548 17.9739 16C17.9739 16.5453 17.7769 17.015 17.3829 17.409C16.9889 17.803 16.5193 18 15.974 18ZM15.974 27.3334C13.0732 27.3334 10.5535 26.3808 8.41508 24.4757C6.27661 22.5705 5.04499 20.1898 4.72021 17.3334H6.76118C7.09794 19.6359 8.13598 21.5449 9.87531 23.0603C11.6146 24.5757 13.6475 25.3334 15.974 25.3334C18.574 25.3334 20.7796 24.4278 22.5907 22.6167C24.4018 20.8056 25.3074 18.6 25.3074 16C25.3074 13.4 24.4018 11.1945 22.5907 9.38338C20.7796 7.57227 18.574 6.66672 15.974 6.66672C14.5176 6.66672 13.1527 6.99022 11.8791 7.63722C10.6057 8.28422 9.50908 9.17438 8.58941 10.3077H12.0766V12.3077H5.30745V5.53852H7.30738V8.69752C8.38943 7.4206 9.68515 6.42958 11.1945 5.72445C12.7039 5.01932 14.2971 4.66675 15.974 4.66675C17.5473 4.66675 19.021 4.96419 20.395 5.55908C21.769 6.15395 22.9671 6.96248 23.9893 7.98468C25.0116 9.0069 25.8201 10.2049 26.415 11.5788C27.0098 12.9527 27.3073 14.4262 27.3073 15.9993C27.3073 17.5724 27.0098 19.0462 26.415 20.4206C25.8201 21.7949 25.0116 22.9932 23.9893 24.0154C22.9671 25.0376 21.769 25.8462 20.395 26.441C19.021 27.0359 17.5473 27.3334 15.974 27.3334Z"
                    fill="#888B8D"
                  />
                </g>
              </svg>
              <span class="font-bold text-gray-400">ล้างค่าทั้งหมด</span>
            </div>
          </button>
        </div>
        <div>
          <div class="flex flex-1 flex-wrap flex-row mt-6">
            <div class="basis-1/5 px-3 mb-3">
              <div class="flex flex-col">
                <label class="label font-bold">Project Type</label>
                <!-- <input
                  type="text"
                  class="h-14 outline outline-2 outline-gray-200 rounded-lg px-3"
                  v-model="formFilter.ProjectID"
                  placeholder="Project Type"
                /> -->
                <select
                  class="h-14 outline outline-2 outline-gray-200 rounded-lg px-3"
                  v-model="formFilter.PMType"
                >
                  <option value="">----------Select----------</option>
                  <option value="P">โครงการ</option>
                  <option value="F">งบการเงิน</option>
                </select>
              </div>
            </div>
            <div class="basis-1/5 px-3 mb-3">
              <div class="flex flex-col">
                <label class="label font-bold">Project ID</label>
                <input
                  type="text"
                  class="h-14 outline outline-2 outline-gray-200 rounded-lg px-3"
                  v-model="formFilter.ProjectID"
                  placeholder="Project ID"
                />
              </div>
            </div>
            <div class="basis-1/5 px-3 mb-3">
              <div class="flex flex-col">
                <label class="label font-bold">Project Name</label>
                <input
                  type="text"
                  class="h-14 outline outline-2 outline-gray-200 rounded-lg px-3"
                  v-model="formFilter.PMName"
                  placeholder="Project Name"
                />
              </div>
            </div>
            <div class="basis-1/5 px-3 mb-3">
              <div class="flex flex-col">
                <label class="label font-bold">PM Name</label>
                <input
                  type="text"
                  class="h-14 outline outline-2 outline-gray-200 rounded-lg px-3"
                  v-model="formFilter.PMInitialName"
                  placeholder="PM Name"
                />
              </div>
            </div>
            <div class="basis-1/5 px-3 mb-3">
              <div class="flex flex-col">
                <label class="label font-bold">Status</label>
                <select
                  class="h-14 outline outline-2 outline-gray-200 rounded-lg px-3"
                  v-model="formFilter.PMStatus"
                >
                  <option value="">----------Select----------</option>
                  <template v-for="(item, index) in DDL.status" :key="index">
                    <option :value="item.StatusID">
                      {{ item.StatusDesc }}
                    </option>
                  </template>
                </select>
                <!-- <input type="text" class="h-14 outline outline-2 outline-gray-200 rounded-lg px-3 "
                                    placeholder="Status"> -->
              </div>
            </div>
            <div class="basis-1/5 px-3 mb-3">
              <div class="flex flex-col">
                <label class="label font-bold">Submit Date From</label>
                <VueDatePicker style=""  :enableTimePicker="false" v-model="formFilter.PMStartDate" :format="format" placeholder="Submit Date From">
                        </VueDatePicker>
                <!-- <input
                  type="date"
                  v-model="formFilter.PMStartDate"
                  placeholder="dd/mm/yyyy"
                  datepicker-format="dd/mm/yyyy"
                  :class="{
                    'px-3 h-14 rounded-lg w-full': true,
                    'outline outline-2 outline-gray-300': true,
                  }"
                /> -->
              </div>
            </div>
            <div class="basis-1/5 px-3 mb-3">
              <div class="flex flex-col">
                <label class="label font-bold">Submit Date To</label>
                <VueDatePicker :enableTimePicker="false" v-model="formFilter.PMEndDate" :format="format" placeholder="Submit Date To">
                        </VueDatePicker>
                <!-- <input
                  type="date"
                  v-model="formFilter.PMEndDate"
                  placeholder="dd/mm/yyyy"
                  datepicker-format="dd/mm/yyyy"
                  :class="{
                    'px-3 h-14 rounded-lg w-full': true,
                    'outline outline-2 outline-gray-300': true,
                  }"
                /> -->
              </div>
            </div>
            <div class="basis-1/5 px-3 mb-3">
              <div class="flex flex-col">
                <label class="label font-bold">Insider Name</label>
                <input
                  type="text"
                  class="h-14 outline outline-2 outline-gray-200 rounded-lg px-3"
                  v-model="formFilter.PMFirstname"
                  placeholder="Insider Name"
                />
              </div>
            </div>
            <!-- <div class="basis-1/4 px-3 mb-3">
              <div class="flex flex-col">
                <label class="label font-bold">Insider LastName</label>
                <input type="text" class="h-14 outline outline-2 outline-gray-200 rounded-lg px-3"
                  v-model="formFilter.PMLastname" placeholder="Insider LastName" />
              </div>
            </div> -->
            <div class="basis-1/5 px-3 mb-3">
              <div class="flex flex-col">
                <label class="label font-bold">Insider Email</label>
                <input
                  type="text"
                  class="h-14 outline outline-2 outline-gray-200 rounded-lg px-3"
                  v-model="formFilter.PMEmail"
                  placeholder="Insider Email"
                />
                <!-- <input type="text" class="h-14 outline outline-2 outline-gray-200 rounded-lg px-3 "
                                    placeholder="Insider Status"> -->
              </div>
            </div>
            <div class="basis-1/5 px-3 mb-3">
              <div class="flex flex-col">
                <label class="label font-bold">Insider Status</label>
                <select
                  class="h-14 outline outline-2 outline-gray-200 rounded-lg px-3"
                  v-model="formFilter.EmpStatus"
                >
                  <option value="">----------Select----------</option>
                  <option value="1">Employee Accepted</option>
                  <option value="0">Waiting Employee Accept</option>
                </select>
                <!-- <input type="text" class="h-14 outline outline-2 outline-gray-200 rounded-lg px-3 "
                                    placeholder="Insider Status"> -->
              </div>
            </div>
          </div>
          <div class="basis-1/5 px-3 mb-3 flex items-end justify-end">
            <button
              class="rounded-full px-10 py-2 bg-custom-blue hover:bg-amber-600t"
              @click="onFilter"
            >
              <div class="flex space-x-3 items-center">
                <svg
                  width="33"
                  class="text-white"
                  height="33"
                  viewBox="0 0 33 33"
                  fill="none"
                  xmlns="http://www.w3.org/2000/svg"
                >
                  <mask
                    id="mask0_246_12511"
                    style="mask-type: alpha"
                    maskUnits="userSpaceOnUse"
                    x="0"
                    y="0"
                    width="33"
                    height="33"
                  >
                    <rect
                      x="0.5"
                      y="0.5"
                      width="32"
                      height="32"
                      fill="#D9D9D9"
                    />
                  </mask>
                  <g mask="url(#mask0_246_12511)">
                    <path
                      d="M4.5 24.5V21.8333H12.5V24.5H4.5ZM4.5 17.8333V15.1667H20.5V17.8333H4.5ZM4.5 11.1667V8.5H28.5V11.1667H4.5Z"
                      fill="#FFFFFF"
                    />
                  </g>
                </svg>
                <span class="text-white font-bold">Filter</span>
              </div>
            </button>
          </div>
          <!-- <div class="flex justify-end px-3">
                        <button class="rounded-full px-8 py-2 bg-custom-blue hover:bg-amber-600t" @click="onFilter">
                            <div class="flex space-x-3 items-center">
                                <svg width="33" class="text-white" height="33" viewBox="0 0 33 33" fill="none"
                                    xmlns="http://www.w3.org/2000/svg">
                                    <mask id="mask0_246_12511" style="mask-type:alpha" maskUnits="userSpaceOnUse" x="0"
                                        y="0" width="33" height="33">
                                        <rect x="0.5" y="0.5" width="32" height="32" fill="#D9D9D9" />
                                    </mask>
                                    <g mask="url(#mask0_246_12511)">
                                        <path
                                            d="M4.5 24.5V21.8333H12.5V24.5H4.5ZM4.5 17.8333V15.1667H20.5V17.8333H4.5ZM4.5 11.1667V8.5H28.5V11.1667H4.5Z"
                                            fill="#FFFFFF" />
                                    </g>
                                </svg>
                                <span class="text-white font-bold">Filter</span>
                            </div>
                        </button>
                    </div> -->
        </div>
        <div class="flex justify-between mt-12 mb-2">
          <div class="flex space-x-3 items-center">
            <svg
              width="29"
              height="20"
              viewBox="0 0 29 20"
              fill="none"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                d="M0.166626 13.8333V11.1667H2.83329V13.8333H0.166626ZM0.166626 8.5V5.83333H2.83329V8.5H0.166626ZM0.166626 3.16667V0.5H2.83329V3.16667H0.166626ZM5.49996 13.8333V11.1667H10.8333V13.8333H5.49996ZM5.49996 8.5V5.83333H22.8333V8.5H5.49996ZM5.49996 3.16667V0.5H22.8333V3.16667H5.49996ZM18.7666 19.1667L13.1 13.5L15 11.6L18.7666 15.3667L26.3 7.83333L28.1666 9.76667L18.7666 19.1667Z"
                fill="#48A23F"
              />
            </svg>
            <span class="text-lg font-bold"
              >Blackout list({{ paginationConfig.totalRows }})</span
            >
          </div>
          <button
            class="rounded-full px-8 py-2 outline outline-blue-700 hover:bg-blue-200"
          >
            <div class="flex space-x-3 items-center">
              <svg
                width="25"
                height="25"
                viewBox="0 0 25 25"
                fill="none"
                xmlns="http://www.w3.org/2000/svg"
              >
                <mask
                  id="mask0_246_12543"
                  style="mask-type: alpha"
                  maskUnits="userSpaceOnUse"
                  x="0"
                  y="0"
                  width="25"
                  height="25"
                >
                  <rect x="0.5" y="0.5" width="24" height="24" fill="#D9D9D9" />
                </mask>
                <g mask="url(#mask0_246_12543)">
                  <path
                    d="M11.5 19.5H13.5V15.325L15.1 16.925L16.5 15.5L12.5 11.5L8.5 15.5L9.925 16.9L11.5 15.325V19.5ZM6.5 22.5C5.95 22.5 5.47917 22.3042 5.0875 21.9125C4.69583 21.5208 4.5 21.05 4.5 20.5V4.5C4.5 3.95 4.69583 3.47917 5.0875 3.0875C5.47917 2.69583 5.95 2.5 6.5 2.5H14.5L20.5 8.5V20.5C20.5 21.05 20.3042 21.5208 19.9125 21.9125C19.5208 22.3042 19.05 22.5 18.5 22.5H6.5ZM13.5 9.5V4.5H6.5V20.5H18.5V9.5H13.5Z"
                    fill="#005B99"
                  />
                </g>
              </svg>
              <span @click="kendoExcelExport()" class="font-bold text-blue-700"
                >รายงานข้อมูล</span
              >
            </div>
          </button>
        </div>
        <div class="overflow-x-auto">
          <table class="table-md rounded-xl">
            <thead>
              <tr class="bg-gray-200">
                <th
                  class="text-md font-bold text-black min-w-[150px]"
                  style="width: 10%"
                >
                  Project ID
                </th>
                <th
                  class="text-md font-bold text-black min-w-[150px]"
                  style="width: 10%"
                >
                  Project Category
                </th>
                <th
                  class="text-md font-bold text-black min-w-[200px]"
                  style="width: 20%"
                >
                  Project Name<br />Insider Name
                </th>
                <th
                  class="text-md font-bold text-black min-w-[150px]"
                  style="width: 15%"
                >
                  PM Name<br />
                  Insider E-mail
                </th>
                <th
                  class="text-md font-bold text-black min-w-[200px]"
                  style="width: 15%"
                >
                  PM Department <br />Insider Position
                </th>
                <th
                  class="text-md font-bold text-black min-w-[150px]"
                  style="width: 15%"
                >
                  Status
                </th>
                <th
                  class="text-md font-bold text-black min-w-[150px]"
                  style="width: 15%"
                >
                  Status Date
                </th>
              </tr>
            </thead>
            <tbody>
              <tr class="border-b-2" v-for="(row, index) in rows" :key="index">
                <td class="font-semibold">{{ row?.ProjectID || "" }}</td>
                <td class="font-semibold">
                  {{ row?.PMType == "P" ? "โครงการ" : "งบการเงิน" }}
                </td>
                <td>
                  <div class="flex flex-col">
                    <span class="font-semibold">
                      {{ row?.PMName || "" }}
                    </span>
                    <span class="">
                      {{ row?.Title }} {{ row?.Firstname }} {{ row?.Lastname }}
                    </span>
                  </div>
                </td>
                <td class="">
                  <div class="flex flex-col">
                    <span class="font-semibold">
                      {{ row?.PMInitialName || "" }}
                    </span>
                    <span class="">
                      {{ row?.Email || "" }}
                    </span>
                  </div>
                </td>
                <td class="">
                  <div class="flex flex-col">
                    <span class="font-semibold">
                      {{ row?.PMDepartment || "" }}
                    </span>
                    <span class="">
                      {{ row?.Position || "" }}
                    </span>
                  </div>
                </td>
                <td>
                  <div class="flex flex-col">
                    <span class="font-semibold">
                      {{ row?.StatusDesc || "" }}
                    </span>
                    <span
                      class="custom-blue text-lime-700 w-64 px-2 py-1 rounded-full font-semibold w-32 text-center"
                      v-if="row.EmpStatus == true"
                    >
                      Employee Accepted
                    </span>
                    <span
                      class="bg-orange-100 w-64 text-orange-700 px-2 py-1 rounded-full font-semibold w-32 text-center"
                      v-else
                    >
                      Waiting Employee Accept
                    </span>
                  </div>
                </td>
                <td>
                  <div class="flex flex-col items-center">
                    <span class="font-semibold"> &nbsp; </span>
                    <span class="font-semibold">
                      {{
                        row?.UpdateDateTime
                          ? formatDate(row?.UpdateDateTime)
                          : ""
                      }}
                    </span>
                  </div>
                </td>
              </tr>
            </tbody>
          </table>
        </div>
        <div class="flex justify-end mt-6">
          <v-pagination
            v-model="paginationConfig.pageIndexNumber"
            :pages="paginationConfig.totalPages"
            :range-size="3"
            active-color="#83cc16"
            @update:modelValue="onLoadData"
          />
          <!-- {{paginationConfig.pageIndexNumber}} -->
          <!-- <div class="join overflow-auto">
                            <template v-for="(item, index) in paginationConfig.totalPages" :key="index  ">
                                <input v-model="paginationConfig.pageIndexNumber" :value="index"
                                    class="join-item btn btn-square" @click="onChangePagination(index)" type="radio"
                                    name="options" :aria-label="index + 1" :checked="index == 0 ? true : false" />
                            </template>
                        </div> -->
        </div>
      </div>
    </div>
  </div>
</template>
<style scoped>
@media (max-width: 1280px) {
  .h-14 {
    height: 3.5rem;
    width: 13rem;
}
}
U
.Page {
  display: flex;
  align-items: center;
  justify-content: center;
  width: 30px;
  height: 30px;
  margin: 0 2px;
  padding: 3px;
  color: #666666;
  background-color: transparent;
  font-size: 18px;
  border-radius: 3px;
  /* box-sizing: border-box; */
  border-color: transparent;
  cursor: pointer;
  outline: 0;
  -webkit-user-select: none;
  -moz-user-select: none;
  user-select: none;
}
</style>
