import { useState } from "react";
import apiClient from "../../services/apiClient";

const useResetButton = () => {
  const [isLoading, setIsLoading] = useState<boolean>(false);

  const handleResetOnClick = async () => {
    setIsLoading(true);

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

  return { isLoading, handleResetOnClick };
};

export default useResetButton;
