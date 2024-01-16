import { Button } from "@mui/material";
import useTimeLogsTable from "./useTimeLogsTable";
import { DataGrid } from "@mui/x-data-grid";
import { parseDate } from "./utils";

const TimeLogsTable = () => {
  const {
    timeLogs,
    paginationModel,
    setPaginationModel,
    rowCountState,
    isLoading,
    compareOnClick,
  } = useTimeLogsTable();

  return (
    <DataGrid
      paginationMode="server"
      pageSizeOptions={[10]}
      paginationModel={paginationModel}
      onPaginationModelChange={setPaginationModel}
      loading={isLoading}
      columns={[
        {
          field: "userName",
          headerName: "User Name",
          width: 150, //TODO: Fix stlyes.
        },
        {
          field: "userEmail",
          headerName: "User Email",
          width: 200,
        },
        {
          field: "projectName",
          headerName: "Project Name",
        },
        {
          field: "date",
          headerName: "Date",
        },
        {
          field: "hours",
          headerName: "Hours Worked",
        },
        {
          field: "action",
          headerName: "Action",
          sortable: false,
          filterable: false,
          renderCell: (params) => {
            return (
              <Button
                onClick={(e) => compareOnClick(e, params.row.id)}
                variant="contained"
              >
                Compare
              </Button>
            );
          },
        },
      ]}
      rows={timeLogs.map((tl) => ({
        ...tl,
        userName: `${tl.userFirstName} ${tl.userLastName}`,
        date: parseDate(tl.date),
      }))}
      rowCount={rowCountState}
    />
  );
};

export default TimeLogsTable;
