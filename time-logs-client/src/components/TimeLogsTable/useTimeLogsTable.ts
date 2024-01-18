import { useEffect, useState } from "react";
import TimeLog from "../../models/time-log.model";
import apiClient from "../../services/apiClient";
import useApplicationContext from "../../context/ApplicationContext";
import User from "../../models/user.model";

const useTimeLogsTable = () => {
  const { dateRangeFilter, handleComparedChartBarChange } =
    useApplicationContext();
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

  const compareOnClick = async (
    e: React.MouseEvent<HTMLButtonElement, MouseEvent>,
    userId: number,
  ) => {
    e.stopPropagation();

    await apiClient<User>({
      url: `User/${userId}`,
      method: "get",
    }).then((response) => {
      const { firstName, lastName, hoursWorked } = response;
      handleComparedChartBarChange({
        label: `${firstName} ${lastName}`,
        value: hoursWorked,
      });
    });
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
          dateRange: {
            dateFrom: dateRangeFilter?.startDate ?? "",
            dateTo: dateRangeFilter?.endDate ?? "",
          },
        },
      }).then((response) => {
        setTimeLogs(response.data);
        setRowCountState(response.totalCount);
        setIsLoading(false);
      });
    })();
  }, [paginationModel, dateRangeFilter]);

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
