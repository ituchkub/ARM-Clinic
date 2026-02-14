<script setup>
import { inject, onMounted, ref, watch } from "vue";
import _apiBuyService from "../../../../api/buyservice.js";
import modalConfirm from "../../../../components/modal-confirm.vue";
import { getTokenStorage } from "../../../../helpers/storage.js";
import { useStore } from "vuex";
const columns = [
  { label: "รหัสรายการ", size: "lg", width: "35%" },
  // { label: "ชื่อสินค้า", size: "lg", width: "25%" },
  // { label: "ราคาสินค้า", size: "lg", width: "25%" },
  // { label: "จำนวน", size: "lg", width: "25%" },
  { label: "ราคารวม", size: "lg", width: "25%" },
  { label: "ชื่อลูกค้า", size: "lg", width: "25%" },
  { label: "", size: "lg", width: "10%" },
];
const $store = useStore()
const sessionInfo = getTokenStorage().sestionInfo;
const rows = ref([]);
const stateModal = ref("add");
const rowDelete = ref({
  row: {},
  index: 0,
});
const IsSuperAdmin = ref([]);
const companyList = ref([]);
const modalErrorLabel = ref("");
const formModalConfirm = ref({
  label: "",
});

import { computed } from 'vue'


const formModal = ref({
  ServiceID: 0,
  ServiceCode: "",
  ServiceName: "",
  ServiceDetail: "",
  ServicePrice: "",
  ServiceType: "",
});

const onClearModal = () => {
  formModal.value = {
    ServiceID: 0,
    ServiceCode: "",
    ServiceName: "",
    ServiceDetail: "",
    ServicePrice: "",
    ServiceType: "",
  };
  modalErrorLabel.value = "";
};
const onOpenModal = (val, row) => {
  if (val === "add") {
    onClearModal();
  } else {
    modalErrorLabel.value = "";
    formModal.value = {
      ServiceID: row.ServiceID,
      ServiceCode: row.ServiceCode,
      ServiceName: row.ServiceName,
      ServiceDetail: row.ServiceDetail,
      ServicePrice: row.ServicePrice,
      ServiceType: row.ServiceType,
    };
  }
  stateModal.value = val;
  document.getElementById("modal-manager").showModal();
};
const onCloseModal = () => {
  document.getElementById("modal-manager").close();
};

const onSubmitModal = async () => {
  if (stateModal.value == "add") {
    const body = rows.value.map(item => ({
      BuyPID: 0,
      ServiceID: item.ServiceID,
      ServiceQTY: item.Qty,
      UserID: selectedCustomer.value.CusID,
      BuyDate: new Date().toISOString()
    }))
  }

};
const onLoadData = async () => {


  await _apiBuyService.getBillList(
    [{ BanchID: getTokenStorage().sestionInfo.BanchID },],
    (response) => {
      rows.value = response.body;
      console.log(response.body)
    }
  )

  $store.commit('setStatusLoading', false);

};
const onClickDelete = (row, index) => {
  formModalConfirm.value.label =
    "ท่านต้องการลบรายการนี้ออกจากระบบใช่หรือไม่ ?";
  rowDelete.value = {
    row: row,
    index: index,
  };
  document.getElementById("modal-confirm").showModal();
};
const onEmitValueConfirm = () => {
  const body = { ServiceID: rowDelete.value.row.ServiceID };
};
onMounted(async () => {
  companyList.value = inject("config").companyLoginList;
  IsSuperAdmin.value = sessionInfo.PMRole == "IT" ? true : false;
  $store.commit('setStatusLoading', true)
  await onLoadData();
});

const grandTotal = computed(() => {
  return rows.value.reduce((sum, item) => {
    return sum + item.Total
  }, 0)
})

watch(
  // () => searchValue.value,
  // async (val) => {
  //   rows.value = rowsOriginal.value.filter((item) => {
  //     return (
  //       item.ServiceCode?.includes(val) ||
  //       item.ServiceName?.includes(val) ||
  //       item.ServiceDetail?.includes(val)
  //     );
  //   });
  // }
);
</script>
<template>
  <div class="flex justify-end space-x-2">
    <input class="bg-gray-200 rounded-full px-8 py-2" placeholder="ค้นหา" v-model="searchValue" />
  </div>
  <!-- ---------------------------------------------------------------------------------------------------------------- -->
  <div class=" overflow-x-auto">
    <table class="table rounded-xl w-full mt-3">
      <thead>
        <tr class="bg-gray-200">
          <th v-for="(item, key) in columns" :key="key" :style="{ width: item.width }">
            <span :class="'text-' + item.size + ' text-black'">{{
              item.label
            }}</span>
          </th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(row, index) in rows" :key="index">

          <td>
            <span class="font-bold">{{
              `${row?.Prefix || ""} ${row?.BillUID || ""
              }`
            }}</span>
          </td>
          <!-- <td>
            <span class="font-bold">{{ row?.ProductName || "" }}</span>
          </td> -->
          <td>
            <span class="font-bold">{{ row?.SumServicePrice || "" }}</span>
          </td>
          <td>
            <span class="font-bold">คุณ{{ row?.Name + " " + row?.Surname || "" }}</span>
          </td>
          <!-- <td>
            <span class="font-bold">{{ row?.ProductQTY * row?.ProductPrice || "" }}</span>
          </td> -->
          <td>
            <div class="flex space-x-3">
              <!-- <button @click="onOpenModal('edit', row)"
                class="rounded-full p-2 outline-none bg-amber-100 hover:bg-amber-200">
                <svg class="w-6 h-6 text-gray-400" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none"
                  viewBox="0 0 24 24">
                  <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                    d="m14.3 4.8 2.9 2.9M7 7H4a1 1 0 0 0-1 1v10c0 .6.4 1 1 1h11c.6 0 1-.4 1-1v-4.5m2.4-10a2 2 0 0 1 0 3l-6.8 6.8L8 14l.7-3.6 6.9-6.8a2 2 0 0 1 2.8 0Z" />
                </svg>
              </button> -->
              <!-- <button @click="onClickDelete(row, index)" class="rounded-full bg-gray-200 p-2 hover:bg-gray-300">
                <svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                  <mask id="mask0_272_1785" style="mask-type: alpha" maskUnits="userSpaceOnUse" x="0" y="0" width="24"
                    height="24">
                    <rect width="24" height="24" fill="#D9D9D9" />
                  </mask>
                  <g mask="url(#mask0_272_1785)">
                    <path
                      d="M7.3077 20.4998C6.80898 20.4998 6.38302 20.3232 6.02982 19.97C5.67661 19.6168 5.5 19.1908 5.5 18.6921V5.99981H4.5V4.49983H8.99997V3.61523H15V4.49983H19.5V5.99981H18.5V18.6921C18.5 19.1972 18.325 19.6248 17.975 19.9748C17.625 20.3248 17.1974 20.4998 16.6922 20.4998H7.3077ZM17 5.99981H6.99997V18.6921C6.99997 18.7818 7.02883 18.8556 7.08652 18.9133C7.14422 18.971 7.21795 18.9998 7.3077 18.9998H16.6922C16.7692 18.9998 16.8397 18.9678 16.9038 18.9037C16.9679 18.8395 17 18.769 17 18.6921V5.99981ZM9.40385 16.9998H10.9038V7.99981H9.40385V16.9998ZM13.0961 16.9998H14.5961V7.99981H13.0961V16.9998Z"
                      fill="#BBBCBC" />
                  </g>
                </svg>
              </button> -->
            </div>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
  <!-- ------------------------------------------------------------------------------------------------------------------------------------- -->
  <dialog id="modal-manager" class="modal">
    <div class="modal-box max-w-xl px-8">
      <div class="flex space-x-3">
        <span class="font-bold text-xl">{{
          stateModal == "add" ? "แจ้งชำระเงิน" : ""
        }}</span>
      </div>
      <button className="btn btn-sm btn-circle btn-ghost absolute right-2 top-2" @click="onCloseModal">
        ✕
      </button>
      <form @submit.prevent="onSubmitModal">
        <div class="mt-3 mb-3">
          <div class="flex flex-wrap flex-row">

          </div>
          <div class="flex justify-between mt-10">
            <div>
              <span class="text-red-500 font-semibold">{{
                modalErrorLabel
              }}</span>
            </div>
            <div class="flex space-x-3">
              <input type="button" @click="onCloseModal"
                class="rounded-full bg-gray-400 hover:bg-gray-500 text-white px-3 py-2 w-40" value="ยกเลิก" />
              <button type="submit" class="rounded-full bg-custom-blue hover:bg-amber-600t text-white px-3 py-2 w-40">
                ชำระเงินเรียบร้อย
              </button>
            </div>
          </div>
        </div>
      </form>
    </div>
  </dialog>
  <modalConfirm :formModalConfirm="formModalConfirm" @emitValue="onEmitValueConfirm" />
</template>
