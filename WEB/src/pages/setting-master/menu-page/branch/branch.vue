<script setup>
import { inject, onMounted, ref, watch } from "vue";
import _apiBranch from "../../../../api/branch.js";
import modalConfirm from "../../../../components/modal-confirm.vue";
import { getTokenStorage } from "../../../../helpers/storage.js";
import thai_provinces from "../../../../assets/address/thai_provinces.json";
import thai_amphures from "../../../../assets/address/thai_amphures.json";
import thai_tambons from "../../../../assets/address/thai_tambons.json";
import { useStore } from "vuex";
const columns = [
  { label: "รหัสสาขา", size: "lg", width: "15%" },
  { label: "ชื่อสาขา", size: "lg", width: "25%" },
  // { label: "วันที่เปิดทำการ", size: "lg", width: "25%" },
  { label: "", size: "lg", width: "5%" },
];
const $store = useStore()
const sessionInfo = getTokenStorage().sestionInfo;
const rows = ref([]);
const rowsOriginal = ref([]);
const stateModal = ref("add");
const searchValue = ref("");
const rowDelete = ref({
  row: {},
  index: 0,
});
const IsSuperAdmin = ref([]);
const companyList = ref([]);
const modalErrorLabel = ref("");
const SelectedAmphures = ref([]);
const SelectedTambons = ref([]);
const formModalConfirm = ref({
  label: "",
});

const formModal = ref({
  BanchID: 0,
  BanchCode: "",
  BanchName: "",
  BanchType: "",
  BanchMobileNumber: "",
  BanchAddress: "",
  Tambons: "",
  Amphures: "",
  Province: "",
  ZipCode: "",
  BanchQRCode: "",
  BanchDate: ""
});


const onClearModal = () => {
  formModal.value = {
    BanchID: 0,
    BanchCode: "",
    BanchName: "",
    BanchType: "",
    BanchMobileNumber: "",
    BanchAddress: "",
    Tambons: "",
    Amphures: "",
    Province: "",
    ZipCode: "",
    BanchQRCode: "",
    BanchDate: ""
  };
  modalErrorLabel.value = "";
};



async function handleProductPics(event) {
  const file = event.target.files[0];
  if (!file) return;

  const base64 = await convertToBase64(file);
  formModal.value.BanchQRCode = base64;
}

function convertToBase64(file) {
  return new Promise((resolve, reject) => {
    const reader = new FileReader();
    reader.onload = () => resolve(reader.result);
    reader.onerror = (error) => reject(error);
    reader.readAsDataURL(file);
  });
}

const handleSelectProvinceChange = () => {
  const selectedId = formModal.value.Province;
  SelectedAmphures.value = thai_amphures.filter(
    (amphures) => amphures.province_id === parseInt(selectedId)
  ).sort((a, b) => a.name_th.localeCompare(b.name_th));;
}
const handleSelectAmphuresChange = () => {
  const selectedId = formModal.value.Amphures;
  SelectedTambons.value = thai_tambons.filter(
    (amphures) => amphures.amphure_id === parseInt(selectedId)
  ).sort((a, b) => a.name_th.localeCompare(b.name_th));;
}
const handleSelectTambonsChange = () => {
  const selectedId = formModal.value.Tambons;
  const ZipCode = thai_tambons.filter(
    (amphures) => amphures.id === parseInt(selectedId)
  ).sort((a, b) => a.name_th.localeCompare(b.name_th));;
  formModal.value.ZipCode = ZipCode[0].zip_code;
}

const onOpenModal = (val, row) => {
  if (val === "add") {
    onClearModal();
  } else {
    modalErrorLabel.value = "";

    const selectedIdA = row.Province;
    SelectedAmphures.value = thai_amphures.filter(
      (amphures) => amphures.province_id === parseInt(selectedIdA)
    );

    const selectedIdB = row.Amphures;
    SelectedTambons.value = thai_tambons.filter(
      (amphures) => amphures.amphure_id === parseInt(selectedIdB)
    );


    formModal.value = row
  }
  stateModal.value = val;
  document.getElementById("modal-manager").showModal();
};
const onCloseModal = () => {
  document.getElementById("modal-manager").close();
};
const onSubmitModal = async () => {
  if (stateModal.value == "add") {
    const body = {
      ...Object.fromEntries(
        Object.entries(formModal.value).map(([key, value]) => [
          key,
          value === null ? null : String(value),
        ])
      ),
    }
    await _apiBranch.create(body, (response) => {
      if (response.Status === true) {
        document.getElementById("modal-manager").close();
        onLoadData();
      } else {
        console.log("error : ", response.ErrorMessage);
      }
    });
    // }
  } else {
    const body = {
      ...Object.fromEntries(
        Object.entries(formModal.value).map(([key, value]) => [
          key,
          value === null ? null : String(value),
        ])
      ),
    }
    console.log(body)
    await _apiBranch.update(body, (response) => {
      if (response.Status === true) {
        document.getElementById("modal-manager").close();
        onLoadData();
      } else {
        console.log("error : ", response.ErrorMessage);
      }
    });
  }

};
const onLoadData = async () => {
  await _apiBranch.getList(
    { CompanyCode: IsSuperAdmin.value ? "" : sessionInfo.CompanyCode },
    (response) => {

      response.body.forEach(store => {
        store.provinceNameTh =
          thai_provinces.find(p => p.id === Number(store.Province))?.name_th || "";
      });

      response.body.forEach(store => {
        store.amphuresNameTh =
          thai_amphures.find(p => p.id === Number(store.Amphures))?.name_th || "";
      });

      response.body.forEach(store => {
        store.tambonsNameTh =
          thai_tambons.find(p => p.id === Number(store.Tambons))?.name_th || "";
      });


      rows.value = response.body;
      rows.value.map((item) => {
        item.RoleID = item.RoleID?.split(",");
      })
      rowsOriginal.value = rows.value;
      $store.commit('setStatusLoading', false);
    }
  )
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
  const body = { BanchID: rowDelete.value.row.BanchID };
  _apiBranch.delete(body, (response) => {
    rows.value.splice(rowDelete.value.index, 1);
    document.getElementById("modal-confirm").close();
  });
};
onMounted(async () => {
  companyList.value = inject("config").companyLoginList;
  IsSuperAdmin.value = sessionInfo.PMRole == "IT" ? true : false;
  $store.commit('setStatusLoading', true)
  await onLoadData();
});
watch(
  () => searchValue.value,
  async (val) => {
    rows.value = rowsOriginal.value.filter((item) => {
      return (
        item.BanchCode?.includes(val) ||
        item.BanchName?.includes(val) ||
        item.BanchDate?.includes(val)
      );
    });
  }
);
</script>
<template>
  <div class="flex justify-end space-x-2">
    <input class="bg-gray-200 rounded-full px-8 py-2" placeholder="ค้นหา" v-model="searchValue" />
    <button class="rounded-full px-8 py-2 bg-custom-blue hover:bg-amber-600t" @click="onOpenModal('add')">
      <div class="flex items-center space-x-2">
        <svg width="15" height="15" viewBox="0 0 15 15" fill="none" xmlns="http://www.w3.org/2000/svg">
          <path d="M6.5 8.5H0.5V6.5H6.5V0.5H8.5V6.5H14.5V8.5H8.5V14.5H6.5V8.5Z" fill="white" />
        </svg>
        <span class="text-white font-bold">เพิ่มข้อมูลสาขา</span>
      </div>
    </button>
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
              `${row?.Prefix || ""} ${row?.BanchCode || ""
              }`
            }}</span>
          </td>
          <td>
            <span class="font-bold">{{ row?.BanchName || "" }}</span>
          </td>
          <!-- <td>
            <span class="font-bold">{{ row?.BanchDate || "" }}</span>
          </td> -->
          <td>
            <div class="flex space-x-3">
              <button @click="onOpenModal('edit', row)"
                class="rounded-full p-2 outline-none bg-amber-100 hover:bg-amber-200">
                <svg class="w-6 h-6 text-gray-400" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none"
                  viewBox="0 0 24 24">
                  <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                    d="m14.3 4.8 2.9 2.9M7 7H4a1 1 0 0 0-1 1v10c0 .6.4 1 1 1h11c.6 0 1-.4 1-1v-4.5m2.4-10a2 2 0 0 1 0 3l-6.8 6.8L8 14l.7-3.6 6.9-6.8a2 2 0 0 1 2.8 0Z" />
                </svg>
              </button>
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
    <div class="modal-box max-w-4xl px-8">
      <div class="flex space-x-3">
        <svg width="43" height="28" viewBox="0 0 48 32" fill="none" xmlns="http://www.w3.org/2000/svg">
          <path
            d="M25 15.8999C25.9667 14.8332 26.7083 13.6165 27.225 12.2499C27.7417 10.8832 28 9.46655 28 7.99988C28 6.53321 27.7417 5.11654 27.225 3.74988C26.7083 2.38321 25.9667 1.16654 25 0.0998779C27 0.366545 28.6667 1.24988 30 2.74988C31.3333 4.24988 32 5.99988 32 7.99988C32 9.99988 31.3333 11.7499 30 13.2499C28.6667 14.7499 27 15.6332 25 15.8999ZM36 31.9999V25.9999C36 24.7999 35.7333 23.6582 35.2 22.5749C34.6667 21.4915 33.9667 20.5332 33.1 19.6999C34.8 20.2999 36.375 21.0749 37.825 22.0249C39.275 22.9749 40 24.2999 40 25.9999V31.9999H36ZM40 17.9999V13.9999H36V9.99988H40V5.99988H44V9.99988H48V13.9999H44V17.9999H40ZM16 15.9999C13.8 15.9999 11.9167 15.2165 10.35 13.6499C8.78333 12.0832 8 10.1999 8 7.99988C8 5.79988 8.78333 3.91654 10.35 2.34988C11.9167 0.783211 13.8 -0.00012207 16 -0.00012207C18.2 -0.00012207 20.0833 0.783211 21.65 2.34988C23.2167 3.91654 24 5.79988 24 7.99988C24 10.1999 23.2167 12.0832 21.65 13.6499C20.0833 15.2165 18.2 15.9999 16 15.9999ZM0 31.9999V26.3999C0 25.2665 0.291667 24.2249 0.875 23.2749C1.45833 22.3249 2.23333 21.5999 3.2 21.0999C5.26667 20.0665 7.36667 19.2915 9.5 18.7749C11.6333 18.2582 13.8 17.9999 16 17.9999C18.2 17.9999 20.3667 18.2582 22.5 18.7749C24.6333 19.2915 26.7333 20.0665 28.8 21.0999C29.7667 21.5999 30.5417 22.3249 31.125 23.2749C31.7083 24.2249 32 25.2665 32 26.3999V31.9999H0ZM16 11.9999C17.1 11.9999 18.0417 11.6082 18.825 10.8249C19.6083 10.0415 20 9.09988 20 7.99988C20 6.89988 19.6083 5.95821 18.825 5.17488C18.0417 4.39154 17.1 3.99988 16 3.99988C14.9 3.99988 13.9583 4.39154 13.175 5.17488C12.3917 5.95821 12 6.89988 12 7.99988C12 9.09988 12.3917 10.0415 13.175 10.8249C13.9583 11.6082 14.9 11.9999 16 11.9999ZM4 27.9999H28V26.3999C28 26.0332 27.9083 25.6999 27.725 25.3999C27.5417 25.0999 27.3 24.8665 27 24.6999C25.2 23.7999 23.3833 23.1249 21.55 22.6749C19.7167 22.2249 17.8667 21.9999 16 21.9999C14.1333 21.9999 12.2833 22.2249 10.45 22.6749C8.61667 23.1249 6.8 23.7999 5 24.6999C4.7 24.8665 4.45833 25.0999 4.275 25.3999C4.09167 25.6999 4 26.0332 4 26.3999V27.9999Z"
            fill="#48A23F" />
        </svg>
        <span class="font-bold text-xl">{{
          stateModal == "add" ? "เพิ่มข้อมูลสาขา" : "แก้ไขข้อมูลสาขา"
        }}</span>
      </div>
      <button className="btn btn-sm btn-circle btn-ghost absolute right-2 top-2" @click="onCloseModal">
        ✕
      </button>
      <form @submit.prevent="onSubmitModal">
        <div class="mt-3 mb-3">
          <div class="flex flex-wrap flex-row">
            <div class="px-3 basis-2/5 mb-3">
              <label class="label font-bold">รหัสสาขา <span style="color: red">*</span></label>
              <input required v-model="formModal.BanchCode" placeholder="รหัสสาขา" type="text"
                class="outline h-10 rounded-lg px-3 outline-gray-300 outline-2 w-full" />
            </div>
            <div class="px-3 basis-3/5 mb-3">
              <label class="label font-bold">ชื่อสาขา <span style="color: red">*</span></label>
              <input required v-model="formModal.BanchName" placeholder="ชื่อสาขา" type="text"
                class="outline h-10 rounded-lg px-3 outline-gray-300 outline-2 w-full" />
            </div>
            <!-- <div class="px-3 basis-2/5 mb-3">
              <label class="label font-bold">วันที่เปิดทำการ <span style="color: red">*</span></label>
              <input required v-model="formModal.BanchDate" placeholder="วันที่เปิดทำการ" type="text"
                class="outline h-10 rounded-lg px-3 outline-gray-300 outline-2 w-full" />
            </div> -->

            <div class="px-3 basis-1/4 mb-3">
              <label class="label font-bold">ที่อยู่ <span style="color: red">*</span></label>
              <input required v-model="formModal.BanchAddress" placeholder="เลขที่" type="text"
                class="outline h-10 rounded-lg px-3 outline-gray-300 outline-2 w-full" />
            </div>
            <div class="px-3 basis-1/4 mb-3">
              <label class="label font-bold">จังหวัด <span style="color: red">*</span></label>
              <select required class="outline h-10 rounded-lg px-3 outline-gray-300 outline-2 w-full"
                v-model="formModal.Province" @change="handleSelectProvinceChange">
                <option value="" disabled selected hidden>เลือกจังหวัด</option>
                <option v-for="province in thai_provinces" :key="province.id" :value="province.id">
                  {{ province.name_th }}
                </option>
              </select>
            </div>
            <div class="px-3 basis-1/4 mb-3">
              <label class="label font-bold">อำเภอ <span style="color: red">*</span></label>
              <select required class="outline h-10 rounded-lg px-3 outline-gray-300 outline-2 w-full"
                v-model="formModal.Amphures" @change="handleSelectAmphuresChange">
                <option value="" disabled selected hidden>เลือกอำเภอ</option>
                <option v-for="amphure in SelectedAmphures" :key="amphure.id" :value="amphure.id">
                  {{ amphure.name_th }}
                </option>
              </select>
            </div>
            <div class="px-3 basis-1/4 mb-3">
              <label class="label font-bold">ตำบล <span style="color: red">*</span></label>
              <select required class="outline h-10 rounded-lg px-3 outline-gray-300 outline-2 w-full"
                v-model="formModal.Tambons" @change="handleSelectTambonsChange">
                <option value="" disabled selected hidden>เลือกตำบล</option>
                <option v-for="province in SelectedTambons" :key="province.id" :value="province.id">
                  {{ province.name_th }}
                </option>
              </select>
            </div>
            <div class="px-3 basis-1/4 mb-3">
              <label class="label font-bold">รหัสไปรษณีย์ <span style="color: red">*</span></label>
              <input required v-model="formModal.ZipCode" placeholder="รหัสไปรษณีย์" type="text"
                class="outline h-10 rounded-lg px-3 outline-gray-300 outline-2 w-full" />
            </div>
            <div class="px-3 basis-1/4 mb-3">
              <label class="label font-bold">เบอร์ติดต่อ <span style="color: red">*</span></label>
              <input required v-model="formModal.BanchMobileNumber" placeholder="เบอร์ติดต่อ" type="text"
                class="outline h-10 rounded-lg px-3 outline-gray-300 outline-2 w-full" />
            </div>
            <div class="px-3 basis-2/4 mb-3">
              <label class="label font-bold">อีเมล</label>
              <input v-model="formModal.BanchEmail" placeholder="อีเมล" type="text"
                class="outline h-10 rounded-lg px-3 outline-gray-300 outline-2 w-full" />
            </div>
            <div class="px-3 basis-1/2 mb-3">
              <label class="label font-bold">เลขที่ผู้เสียภาษี </label>
              <input v-model="formModal.BanchTaxID" placeholder="เลขที่ผู้เสียภาษี" type="text"
                class="outline h-10 rounded-lg px-3 outline-gray-300 outline-2 w-full" />
            </div>
            <div class="px-3 basis-2/4 mb-3">
              <label class="label font-bold">QRCode <span style="color: red">*</span></label>
              <input type="file" accept="image/*" @change="handleProductPics"
                class="outline h-10 rounded-lg px-3 outline-gray-300 outline-2 w-full bg-white py-2" />
              <div v-if="formModal.BanchQRCode && formModal.BanchQRCode.length > 0">
                <img :src="formModal.BanchQRCode" alt="Preview" class="mt-2 w-full h-40 object-cover" />
              </div>
              <div v-else>
                No image available.
              </div>
            </div>
          </div>
          <div class="flex justify-between mt-10">
            <div>
              <span class="text-red-500 font-semibold">{{
                modalErrorLabel
              }}</span>
            </div>
            <div class="flex space-x-3">
              <input type="button" @click="onCloseModal"
                class="rounded-full bg-gray-400 hover:bg-gray-500 text-white px-3 py-2 w-40" value="Cancel" />
              <button type="submit" class="rounded-full bg-custom-blue hover:bg-amber-600t text-white px-3 py-2 w-40">
                Save
              </button>
            </div>
          </div>
        </div>
      </form>
    </div>
  </dialog>
  <modalConfirm :formModalConfirm="formModalConfirm" @emitValue="onEmitValueConfirm" />
</template>
