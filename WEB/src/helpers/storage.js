const setTokenStorage = ({ token, sestionInfo }) => {
  localStorage.setItem("token", token);
  localStorage.setItem("sestionInfo", JSON.stringify(sestionInfo));
  // localStorage.setItem("azureDetail", JSON.stringify(azureDetail));
};
const getTokenStorage = () => {
  const token = localStorage?.getItem("token");
  const sestionInfo = localStorage?.getItem("sestionInfo");
  // const azureDetail = localStorage?.getItem("azureDetail");
  // console.log("getTokenStorage", token);
  // console.log("getTokenStorage", sestionInfo);
  return { sestionInfo: JSON.parse(sestionInfo), token: token };
};
const removeTokenStorage = () => {
  localStorage.removeItem("token");
  localStorage.removeItem("sestionInfo");
  localStorage.removeItem("azureDetail");
};
export { setTokenStorage, getTokenStorage, removeTokenStorage };
