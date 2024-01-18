import { useEffect, useState } from "react";
import User from "../../models/user.model";
import Project from "../../models/project.model";
import apiClient from "../../services/apiClient";
import { ChartResourceType } from "./utils";
import useApplicationContext from "../../context/ApplicationContext";

const useContent = () => {
  const { dateRangeFilter } = useApplicationContext();
  const [topUsers, setTopUsers] = useState<User[]>([]);
  const [topProjects, setTopProjects] = useState<Project[]>([]);
  const [selectedResource, setSelectedResource] = useState<ChartResourceType>(
    ChartResourceType.Users,
  );

  const handleSelectedResourceOnChange = (
    event: React.ChangeEvent<HTMLInputElement>,
    value: string,
  ) => {
    setSelectedResource(value as ChartResourceType);
  };

  useEffect(() => {
    (async () => {
      await apiClient<User[]>({
        url: "User",
        method: "get",
        queryParams: {
          dateRange: {
            dateFrom: dateRangeFilter?.startDate ?? "",
            dateTo: dateRangeFilter?.endDate ?? "",
          },
        },
      }).then((response) =>
        setTopUsers(
          response.sort((a, b) => b.hoursWorked - a.hoursWorked).slice(0, 10),
        ),
      );

      await apiClient<Project[]>({
        url: "Project",
        method: "get",
        queryParams: {
          dateRange: {
            dateFrom: dateRangeFilter?.startDate ?? "",
            dateTo: dateRangeFilter?.endDate ?? "",
          },
        },
      }).then((response) =>
        setTopProjects(
          response.sort((a, b) => b.hoursWorked - a.hoursWorked).slice(0, 10),
        ),
      );
    })();
  }, [dateRangeFilter]);

  return {
    topUsers,
    topProjects,
    selectedResource,
    handleSelectedResourceOnChange,
  };
};

export default useContent;
