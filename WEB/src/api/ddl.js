import Axios from "axios";
const apiLine = `/Master/DDL`;
export default {
  config_authen() {
    return {
      "Content-Type": "application/json",
      headers: {
        Authorization: `Bearer ${localStorage.getItem("token")}`,
      },
    };
  },
  DDLBarchList(params, success) {
    const sessionInfo = JSON.parse(localStorage.getItem("sestionInfo"));
    const body = {
      sessionEmpID: sessionInfo?.EmpId || 0,
      body: params,
      module: "DDL_BarchList",
    };
    Axios.post(apiLine, body, this.config_authen())
      .then((response) => {
        success(response.data);
      })
      .catch((response) => {
        success(response.data);
      });
  },
  DDLProductList(params, success) {
    const sessionInfo = JSON.parse(localStorage.getItem("sestionInfo"));
    const body = {
      sessionEmpID: sessionInfo?.EmpId || 0,
      body: params,
      module: "DDL_ProductList",
    };
    Axios.post(apiLine, body, this.config_authen())
      .then((response) => {
        success(response.data);
      })
      .catch((response) => {
        success(response.data);
      });
  },
};
