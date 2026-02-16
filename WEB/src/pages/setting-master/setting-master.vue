<script setup>
import { getTokenStorage } from '../../helpers/storage';
import { inject, onMounted, ref } from "vue";
import { useRouter } from "vue-router";
import branch from "./menu-page/branch/branch.vue";
import customer from "./menu-page/customer/customer.vue";
import employee from "./menu-page/employee/employee.vue";
import product from "./menu-page/product/product.vue";
import productStock from "./menu-page/productStock/productStock.vue";
import sumProductStock from "./menu-page/sumProductStock/sumProductStock.vue";
import service from "./menu-page/service/service.vue";

import buyproduct from "./menu-page/buyproduct/buyproduct.vue";
import buyservice from "./menu-page/buyservice/buyservice.vue";

import logbuyproduct from "./menu-page/logbuyproduct/logbuyproduct.vue";
import logbuyservice from "./menu-page/logbuyservice/logbuyservice.vue";

import useservice from "./menu-page/useservice/useservice.vue";

const RoleId = ref([])
const pageOptions = ref({})
const menuOpen = ref(0)
const menuActive = ref('3')

// var menuListReport = [
//     { menuName: "ภาพรวม", Type: "1" },
//     { menuName: "จำนวนลูกค้า", Type: "1" },
//     // { menuName: "สรุปยอดการสั่งซื้อ", Type: "1" },
//     { menuName: "สรุปยอดขาย Offline", Type: "1" },
//     { menuName: "สรุปยอดคําสั่งซื้อ Offline", Type: "1" },
//     { menuName: "สต๊อกสินค้า Offline", Type: "1" },
//     { menuName: "สรุปยอดขาย Online", Type: "1" },
//     { menuName: "สรุปยอดคําสั่งซื้อ Online", Type: "1" },
//     { menuName: "สต๊อกสินค้า Online", Type: "1" },
//     { menuName: "สต็อกวัสดุ,บรรจุภัณฑ์", Type: "1" },
//     { menuName: "สรุปรายรับ", Type: "1" },
//     { menuName: "สรุปรายจ่าย", Type: "1" },
//     { menuName: "สรุปเรียกคืนสินค้า", Type: "1" },
// ]

// var menuListProduct = [
//     { menuName: "รายการสินค้า Offline", Type: "2" },
//     { menuName: "รายการสินค้า Online", Type: "2" },
//     { menuName: "ราคาสินค้า Offline", Type: "2" },
//     { menuName: "ราคาสินค้า Online", Type: "2" },
//     { menuName: "Stock สินค้า Offline", Type: "2" },
//     { menuName: "Stock สินค้า Online", Type: "2" },
//     { menuName: "รายการขาย Offline", Type: "2" },
//     { menuName: "รายการขาย Online", Type: "2" },
//     // { menuName: "เรียกคืนสินค้า", Type: "2" },
//     { menuName: "ตารางคิวรถ", Type: "2" }
// ]

// var menuListMat = [
//     { menuName: "สต๊อกวัตถุดิบและบรรจุภัณฑ์", Type: "3" },
//     { menuName: "สั่งซื้อวัตถุดิบและบรรจุภัณฑ์", Type: "3" },
//     { menuName: "วัตถุดิบและบรรจุภัณฑ์ตีกลับ", Type: "3" },
//     { menuName: "วัตถุดิบและบรรจุภัณฑ์เสียหาย", Type: "3" },
//     { menuName: "วัตถุดิบและบรรจุภัณฑ์ใช้แล้ว", Type: "3" },
// ]

// var menuListDoc = [
//     { menuName: "ใบเสนอราคา", Type: "4" },
//     { menuName: "ใบเสร็จรับเงิน ", Type: "4" },
//     { menuName: "ใบแจ้งหนี้", Type: "4" },
//     { menuName: "ใบส่งสินค้า", Type: "4" },
// ]

var menuListMainSummarize = [
    { menuName: "สรุปยอดคลังสินค้า", Type: "2" },
]


var menuListMainService = [
    { menuName: "ซื้อสินค้า", Type: "3" },
    { menuName: "ซื้อคอร์ส ผิวใสไร้ฝ้ากระ", Type: "3" },
    { menuName: "ประวัติการซื้อสินค้า", Type: "3" },
    { menuName: "ประวัติการซื้อคอร์ส ผิวใสไร้ฝ้ากระ", Type: "3" },
    { menuName: "ใช้คอร์ส ผิวใสไร้ฝ้ากระ", Type: "3" },
]


var menuListMainStock = [
    { menuName: "คลังสินค้า", Type: "4" },
]

var menuListMain = [
    { menuName: "พนักงาน", Type: "5" },
    { menuName: "ลูกค้า", Type: "5" },
    { menuName: "สินค้า", Type: "5" },
    { menuName: "คอร์ส ผิวใสไร้ฝ้ากระ", Type: "5" },
]


var menuListMainAdmin = [
    { menuName: "สาขา", Type: "6" },
]

// var menuListz = []
const $router = useRouter()
const onChangeMenu = (val) => {
    $router.push({
        path: '/',
        query: { page: val }
    })
    menuOpen.value = val

    console.log(val)
}
const beforeEnter = (el) => {

    el.style.height = '0px';  // Start with a height of 0
    el.style.opacity = 0;  // Start with 0 opacity
}
const enter = (el, done) => {

    el.offsetHeight; // Trigger reflow to apply transition
    el.style.transition = 'height 0.3s ease, opacity 0.3s ease'; // Define transition
    el.style.height = 'auto';  // Expand to natural height
    el.style.opacity = 1;  // Fade in
    done();
}
const leave = (el, done) => {

    el.style.transition = 'height 0.3s ease, opacity 0.3s ease'; // Define transition
    el.style.height = '0px';  // Collapse to 0 height
    el.style.opacity = 0;  // Fade out
    done();
}
const onActiveMenu = (val) => {

    menuActive.value = val
}

const onClickImportInsider = () => {
    document.getElementById('modal-import-insiderlist').showModal()
}
onMounted(async () => {

    const result = getTokenStorage()
    console.log("getTokenStorage", result.sestionInfo.RoleId);
    RoleId.value = result.sestionInfo.RoleId
    pageOptions.value = inject('pageOptions')
    menuOpen.value = $router.currentRoute.value.query.page
    if (!menuOpen.value) {
        menuOpen.value = 'ซื้อสินค้า'
    }
    $router.push({
        path: '/',
        query: { page: menuOpen.value }
    })
})
</script>
<template>
    <div class="px-6 mt-8">
        <div class="grid mt-8 grid-cols-7 gap-3  ">
            <div class="bg-white rounded-lg shadow ">

                <!-- 
                <div v-if="!RoleId || RoleId.includes('รายงานสรุป')" @click="onActiveMenu('1')"
                    class="px-3 pt-3 flex space-x-2 items-center cursor-pointer">
                    <svg xmlns="http://www.w3.org/2000/svg" x="0px" y="0px" class="w-4 h-4" viewBox="0 0 50 50">
                        <path
                            d="M 0 9 L 0 11 L 50 11 L 50 9 Z M 0 24 L 0 26 L 50 26 L 50 24 Z M 0 39 L 0 41 L 50 41 L 50 39 Z">
                        </path>
                    </svg>
                    <span class="font-bold text-lg">รายงานสรุป</span>
                </div>
                <transition v-if="!RoleId || RoleId.includes('รายงานสรุป')" name="menu-fade" @before-enter="beforeEnter"
                    @enter="enter" @leave="leave">
                    <ul v-if="menuActive == '1'" class="menu w-full rounded-box pl-2">
                        <li @click="onChangeMenu(item.menuName)"
                            :class="{ 'rounded-lg font-bold': true, 'bg-custom-blue text-white': menuOpen === item.menuName }"
                            v-for="(item, index) in menuListReport" :key="index">
                            <a>

                                <i class="bi bi-clipboard-data text-gray-600 text-xl"></i>
                                {{ item.menuName }}
                            </a>
                        </li>
                    </ul>
                </transition>
                <div v-if="!RoleId || RoleId.includes('การจัดการสินค้า')" @click="onActiveMenu('2')"
                    class="px-3 pt-3 flex space-x-2 items-center cursor-pointer">
                    <svg xmlns="http://www.w3.org/2000/svg" x="0px" y="0px" class="w-4 h-4" viewBox="0 0 50 50">
                        <path
                            d="M 0 9 L 0 11 L 50 11 L 50 9 Z M 0 24 L 0 26 L 50 26 L 50 24 Z M 0 39 L 0 41 L 50 41 L 50 39 Z">
                        </path>
                    </svg>
                    <span class="font-bold text-lg">การจัดการสินค้า
                    </span>
                </div>
                <transition name="menu-fade" @before-enter="beforeEnter" @enter="enter" @leave="leave">
                    <ul v-if="menuActive == '2'" class="menu w-full rounded-box pl-2">
                        <li @click="onChangeMenu(item.menuName)"
                            :class="{ 'rounded-lg font-bold': true, 'bg-custom-blue text-white': menuOpen === item.menuName }"
                            v-for="(item, index) in menuListProduct" :key="index">
                            <a>
                                <i class="bi bi-clipboard-data text-gray-600 text-xl"></i>
                                <div style="font-size: 14px;">{{ item.menuName }}</div>
                            </a>
                        </li>
                    </ul>
                </transition>
                <div v-if="!RoleId || RoleId.includes('ค่าใช้จ่ายเพิ่มเติม')"
                    class="px-3 pt-3 flex space-x-2 items-center cursor-pointer">
                    <svg xmlns="http://www.w3.org/2000/svg" x="0px" y="0px" class="w-4 h-4" viewBox="0 0 50 50">
                        <path
                            d="M 0 9 L 0 11 L 50 11 L 50 9 Z M 0 24 L 0 26 L 50 26 L 50 24 Z M 0 39 L 0 41 L 50 41 L 50 39 Z">
                        </path>
                    </svg>
                    <span class="font-bold text-lg"
                        @click="onChangeMenu('ค่าใช้จ่ายเพิ่มเติม'); onActiveMenu('0')">ค่าใช้จ่ายเพิ่มเติม</span>
                </div>
                <div v-if="!RoleId || RoleId.includes('รายการเดินรถขนส่ง')"
                    class="px-3 pt-3 flex space-x-2 items-center cursor-pointer">
                    <svg xmlns="http://www.w3.org/2000/svg" x="0px" y="0px" class="w-4 h-4" viewBox="0 0 50 50">
                        <path
                            d="M 0 9 L 0 11 L 50 11 L 50 9 Z M 0 24 L 0 26 L 50 26 L 50 24 Z M 0 39 L 0 41 L 50 41 L 50 39 Z">
                        </path>
                    </svg>
                    <span class="font-bold text-lg"
                        @click="onChangeMenu('รายการเดินรถขนส่ง'); onActiveMenu('0')">รายการเดินรถขนส่ง</span>
                </div>
                <div v-if="!RoleId || RoleId.includes('วัสดุบรรจุภัณฑ์')" @click="onActiveMenu('3')"
                    class="px-3 pt-3 flex space-x-2 items-center cursor-pointer">
                    <svg xmlns="http://www.w3.org/2000/svg" x="0px" y="0px" class="w-4 h-4" viewBox="0 0 50 50">
                        <path
                            d="M 0 9 L 0 11 L 50 11 L 50 9 Z M 0 24 L 0 26 L 50 26 L 50 24 Z M 0 39 L 0 41 L 50 41 L 50 39 Z">
                        </path>
                    </svg>
                    <span class="font-bold text-lg">วัสดุ,บรรจุภัณฑ์
                    </span>
                </div>
                <transition name="menu-fade" @before-enter="beforeEnter" @enter="enter" @leave="leave">
                    <ul v-if="menuActive == '3'" class="menu w-full rounded-box pl-2">
                        <li @click="onChangeMenu(item.menuName)"
                            :class="{ 'rounded-lg font-bold': true, 'bg-custom-blue text-white': menuOpen === item.menuName }"
                            v-for="(item, index) in menuListMat" :key="index">
                            <a>
                                <i class="bi bi-clipboard-data text-gray-600 text-xl"></i>
                                <div style="font-size: 14px;">{{ item.menuName }}</div>
                            </a>
                        </li>
                    </ul>
                </transition>
                <div v-if="!RoleId || RoleId.includes('เอกสาร')" @click="onActiveMenu('4')"
                    class="px-3 pt-3 flex space-x-2 items-center cursor-pointer">
                    <svg xmlns="http://www.w3.org/2000/svg" x="0px" y="0px" class="w-4 h-4" viewBox="0 0 50 50">
                        <path
                            d="M 0 9 L 0 11 L 50 11 L 50 9 Z M 0 24 L 0 26 L 50 26 L 50 24 Z M 0 39 L 0 41 L 50 41 L 50 39 Z">
                        </path>
                    </svg>
                    <span class="font-bold text-lg">เอกสาร</span>
                </div>
                <transition name="menu-fade" @before-enter="beforeEnter" @enter="enter" @leave="leave">
                    <ul v-if="menuActive == '4'" class="menu w-full rounded-box pl-2">
                        <li @click="onChangeMenu(item.menuName)"
                            :class="{ 'rounded-lg font-bold': true, 'bg-custom-blue text-white': menuOpen === item.menuName }"
                            v-for="(item, index) in menuListDoc" :key="index">
                            <a>

                                <i class="bi bi-clipboard-data text-gray-600 text-xl"></i>
                                <div style="font-size: 14px;">{{ item.menuName }}</div>
                            </a>
                        </li>
                    </ul>
                </transition> -->


                <div v-if="!RoleId || RoleId.includes('เมนูหลัก')" @click="onActiveMenu('3')"
                    class="px-3 pt-3 flex space-x-2 items-center cursor-pointer">
                    <svg xmlns="http://www.w3.org/2000/svg" x="0px" y="0px" class="w-4 h-4" viewBox="0 0 50 50">
                        <path
                            d="M 0 9 L 0 11 L 50 11 L 50 9 Z M 0 24 L 0 26 L 50 26 L 50 24 Z M 0 39 L 0 41 L 50 41 L 50 39 Z">
                        </path>
                    </svg>
                    <span class="font-bold text-lg">เมนูหลัก</span>
                </div>
                <transition name="menu-fade" @before-enter="beforeEnter" @enter="enter" @leave="leave">
                    <ul v-if="menuActive == '3'" class="menu w-full rounded-box pl-2">

                        <li @click="onChangeMenu(item.menuName)"
                            :class="{ 'rounded-lg font-bold': true, 'bg-custom-blue text-white': menuOpen === item.menuName }"
                            v-for="(item, index) in menuListMainService" :key="index">
                            <a>

                                <i class="bi bi-clipboard-data text-gray-600 text-xl"></i>
                                <div style="font-size: 14px;">{{ item.menuName }}</div>
                            </a>
                        </li>
                    </ul>
                </transition>




                <div v-if="!RoleId || RoleId.includes('รายการสรุป')" @click="onActiveMenu('2')"
                    class="px-3 pt-3 flex space-x-2 items-center cursor-pointer">
                    <svg xmlns="http://www.w3.org/2000/svg" x="0px" y="0px" class="w-4 h-4" viewBox="0 0 50 50">
                        <path
                            d="M 0 9 L 0 11 L 50 11 L 50 9 Z M 0 24 L 0 26 L 50 26 L 50 24 Z M 0 39 L 0 41 L 50 41 L 50 39 Z">
                        </path>
                    </svg>
                    <span class="font-bold text-lg">รายการสรุป</span>
                </div>
                <transition name="menu-fade" @before-enter="beforeEnter" @enter="enter" @leave="leave">
                    <ul v-if="menuActive == '2'" class="menu w-full rounded-box pl-2">

                        <li @click="onChangeMenu(item.menuName)"
                            :class="{ 'rounded-lg font-bold': true, 'bg-custom-blue text-white': menuOpen === item.menuName }"
                            v-for="(item, index) in menuListMainSummarize" :key="index">
                            <a>

                                <i class="bi bi-clipboard-data text-gray-600 text-xl"></i>
                                <div style="font-size: 14px;">{{ item.menuName }}</div>
                            </a>
                        </li>
                    </ul>
                </transition>


                <div v-if="!RoleId || RoleId.includes('คลังสินค้า')" @click="onActiveMenu('4')"
                    class="px-3 pt-3 flex space-x-2 items-center cursor-pointer">
                    <svg xmlns="http://www.w3.org/2000/svg" x="0px" y="0px" class="w-4 h-4" viewBox="0 0 50 50">
                        <path
                            d="M 0 9 L 0 11 L 50 11 L 50 9 Z M 0 24 L 0 26 L 50 26 L 50 24 Z M 0 39 L 0 41 L 50 41 L 50 39 Z">
                        </path>
                    </svg>
                    <span class="font-bold text-lg">คลังสินค้า</span>
                </div>
                <transition name="menu-fade" @before-enter="beforeEnter" @enter="enter" @leave="leave">
                    <ul v-if="menuActive == '4'" class="menu w-full rounded-box pl-2">

                        <li @click="onChangeMenu(item.menuName)"
                            :class="{ 'rounded-lg font-bold': true, 'bg-custom-blue text-white': menuOpen === item.menuName }"
                            v-for="(item, index) in menuListMainStock" :key="index">
                            <a>

                                <i class="bi bi-clipboard-data text-gray-600 text-xl"></i>
                                <div style="font-size: 14px;">{{ item.menuName }}</div>
                            </a>
                        </li>
                    </ul>
                </transition>

                <div v-if="!RoleId || RoleId.includes('การจัดการข้อมูล')" @click="onActiveMenu('5')"
                    class="px-3 pt-3 flex space-x-2 items-center cursor-pointer">
                    <svg xmlns="http://www.w3.org/2000/svg" x="0px" y="0px" class="w-4 h-4" viewBox="0 0 50 50">
                        <path
                            d="M 0 9 L 0 11 L 50 11 L 50 9 Z M 0 24 L 0 26 L 50 26 L 50 24 Z M 0 39 L 0 41 L 50 41 L 50 39 Z">
                        </path>
                    </svg>
                    <span class="font-bold text-lg">การจัดการข้อมูล</span>
                </div>
                <transition name="menu-fade" @before-enter="beforeEnter" @enter="enter" @leave="leave">
                    <ul v-if="menuActive == '5'" class="menu w-full rounded-box pl-2">

                        <li @click="onChangeMenu(item.menuName)"
                            :class="{ 'rounded-lg font-bold': true, 'bg-custom-blue text-white': menuOpen === item.menuName }"
                            v-for="(item, index) in menuListMain" :key="index">
                            <a>

                                <i class="bi bi-clipboard-data text-gray-600 text-xl"></i>
                                <div style="font-size: 14px;">{{ item.menuName }}</div>
                            </a>
                        </li>
                    </ul>
                </transition>

                <div v-if="!RoleId || RoleId.includes('การจัดการข้อมูล (ผู้ดูแล)')" @click="onActiveMenu('6')"
                    class="px-3 pt-3 flex space-x-2 items-center cursor-pointer">
                    <svg xmlns="http://www.w3.org/2000/svg" x="0px" y="0px" class="w-4 h-4" viewBox="0 0 50 50">
                        <path
                            d="M 0 9 L 0 11 L 50 11 L 50 9 Z M 0 24 L 0 26 L 50 26 L 50 24 Z M 0 39 L 0 41 L 50 41 L 50 39 Z">
                        </path>
                    </svg>
                    <span class="font-bold text-lg">การจัดการข้อมูล (ผู้ดูแล)</span>
                </div>
                <transition name="menu-fade" @before-enter="beforeEnter" @enter="enter" @leave="leave">
                    <ul v-if="menuActive == '6'" class="menu w-full rounded-box pl-2">

                        <li @click="onChangeMenu(item.menuName)"
                            :class="{ 'rounded-lg font-bold': true, 'bg-custom-blue text-white': menuOpen === item.menuName }"
                            v-for="(item, index) in menuListMainAdmin" :key="index">
                            <a>

                                <i class="bi bi-clipboard-data text-gray-600 text-xl"></i>
                                <div style="font-size: 14px;">{{ item.menuName }}</div>
                            </a>
                        </li>
                    </ul>
                </transition>
            </div>
            <div class="bg-white rounded-lg shadow col-span-6 bg-white px-8 py-6 overflow-auto w-full"
                :style="{ height: (pageOptions && pageOptions.pageHeight ? (pageOptions.pageHeight - 120) + 'px' : 'auto') }">
                <branch v-if="menuOpen === 'สาขา'" />
                <customer v-if="menuOpen === 'ลูกค้า'" />
                <employee v-if="menuOpen === 'พนักงาน'" />
                <product v-if="menuOpen === 'สินค้า'" />
                <productStock v-if="menuOpen === 'คลังสินค้า'" />
                <sumProductStock v-if="menuOpen === 'สรุปยอดคลังสินค้า'" />
                <service v-if="menuOpen === 'คอร์ส ผิวใสไร้ฝ้ากระ'" />

                <buyproduct v-if="menuOpen === 'ซื้อสินค้า'" />
                <buyservice v-if="menuOpen === 'ซื้อคอร์ส ผิวใสไร้ฝ้ากระ'" />

                <logbuyproduct v-if="menuOpen === 'ประวัติการซื้อสินค้า'" />
                <logbuyservice v-if="menuOpen === 'ประวัติการซื้อคอร์ส ผิวใสไร้ฝ้ากระ'" />


                <useservice v-if="menuOpen === 'ใช้คอร์ส ผิวใสไร้ฝ้ากระ'" />


            </div>
        </div>
    </div>
    <!-- ---------------------------------------------------------------------------- -->
</template>
