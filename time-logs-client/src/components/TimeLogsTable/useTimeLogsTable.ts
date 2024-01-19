import { useEffect, useState } from "react";
import TimeLog from "../../models/time-log.model";
import apiClient from "../../services/apiClient";
import useApplicationContext from "../../context/ApplicationContext";
import User from "../../models/user.model";

const useTimeLogsTable = () => {
  const { dateRangeFilter, handleComparedChartBarChange } =
    useApplicationContext();
  const [timeLogs, setTimeLogs] = useState<TimeLog[]>([]);
  const [selectedUserId, setSelectedUserId] = useState<number | null>(null);

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

    setSelectedUserId(userId);

    await apiClient<User>({
      url: `User/${userId}`,
      method: "get",
      queryParams: {
        dateRange: {
          dateFrom: dateRangeFilter?.startDate ?? "",
          dateTo: dateRangeFilter?.endDate ?? "",
        },
      },
    }).then((response) =>
      handleComparedChartBarChange({
        label: response.email,
        value: response.hoursWorked,
      }),
    );
  };

  const unselectOnClick = () => {
    setSelectedUserId(null);
    handleComparedChartBarChange(null);
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

  useEffect(() => {
    setSelectedUserId(null);
    setPaginationModel({
      page: 0,
      pageSize: 10,
    });
  }, [dateRangeFilter]);

  return {
    timeLogs,
    selectedUserId,
    paginationModel,
    setPaginationModel,
    rowCountState,
    isLoading,
    compareOnClick,
    unselectOnClick,
  };
};

export default useTimeLogsTable;
