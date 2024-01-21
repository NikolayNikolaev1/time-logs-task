import apiClient from "../../services/apiClient";
import useApplicationContext from "../../context/ApplicationContext";

const useResetButton = () => {
  const { startLoading } = useApplicationContext();

  const handleResetOnClick = async () => {
    startLoading();

    await apiClient({
      url: "Clear",
      method: "delete",
    }).then(async () => {
      await apiClient({
        url: "Generate",
        method: "post",
      }).then(() => window.location.reload());
    });
  };

  return { handleResetOnClick };
};

export default useResetButton;
