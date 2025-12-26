import Axios from "axios";
const apiLine = `/Master/ServiceList`;
const configAuth = () => ({
  "Content-Type": "application/json",
  headers: {
    Authorization: `Bearer ${localStorage.getItem("token")}`,
  },
});
const createRequestBody = (params, module) => {
  const sessionInfo = JSON.parse(localStorage.getItem("sessionInfo")) || {};
  return {
    sessionEmpID: sessionInfo.EmpId || 0,
    body: params,
    module,
  };
};
const postRequest = (module, params, success, error) => {
  const body = createRequestBody(params, module);
  Axios.post(apiLine, body, configAuth())
    .then((response) => {
      if (success) success(response.data);
    })
    .catch((err) => {
      if (error) error(err.response?.data || err.message);
    });
};
export default {
  getList(params, success, error) {
    postRequest("getlist", params, success, error);
  },
  create(params, success, error) {
    postRequest("upsert", params, success, error);
  },
  update(params, success, error) {
    postRequest("upsert", params, success, error);
  },
  delete(params, success, error) {
    postRequest("delete", params, success, error);
  },
};
