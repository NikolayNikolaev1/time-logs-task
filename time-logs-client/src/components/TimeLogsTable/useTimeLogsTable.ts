import { useEffect, useState } from "react";
import TimeLog from "../../models/time-log.model";
import apiClient from "../../services/apiClient";

const useTimeLogsTable = () => {
  const [timeLogs, setTimeLogs] = useState<TimeLog[]>([]);

  const [paginationModel, setPaginationModel] = useState<{
    page: number;
    pageSize: number;
  }>({
    page: 0,
    pageSize: 10,
  });

  const [isLoading, setIsLoading] = useState<boolean>(true);

  const [rowCountState, setRowCountState] = useState<number>(10);

  const compareOnClick = (
    e: React.MouseEvent<HTMLButtonElement, MouseEvent>,
    timeLogId: number,
  ) => {
    e.stopPropagation();
    console.log({ tester: timeLogId });
  };

  useEffect(() => {
    setIsLoading(true);

    (async () => {
      await apiClient<{
        data: TimeLog[];
        totalCount: number;
      }>({
        url: "TimeLog",
        method: "get",
        queryParams: {
          page: paginationModel.page + 1,
          // TODO: Date filter
        },
      }).then((response) => {
        setTimeLogs(response.data);
        setRowCountState(response.totalCount);
        setIsLoading(false);
      });
    })();
  }, [paginationModel]);

  return {
    timeLogs,
    paginationModel,
    setPaginationModel,
    rowCountState,
    isLoading,
    compareOnClick,
  };
};

export default useTimeLogsTable;
