import queryString from "query-string";

const baseUrl = "https://localhost:7172/api";

interface ApiClientProps {
  url: string;
  method: string;
  queryParams?: {
    dateRange?: {
      dateFrom: string;
      dateTo: string;
    };
    page?: number;
  };
}

const apiClient = async <T>({
  url,
  method,
  queryParams,
}: ApiClientProps): Promise<T> => {
  const query = {
    dateFrom: queryParams?.dateRange?.dateFrom,
    dateTo: queryParams?.dateRange?.dateTo,
    page: queryParams?.page,
  };

  return await fetch(`${baseUrl}/${url}?${queryString.stringify(query)}`, {
    method,
    mode: "cors",
    headers: {
      "Content-Type": "application/json",
    },
  }).then((response) => {
    if (!response.ok) {
      throw { statusCode: response.status };
    }

    if (response.status === 204) return;

    return response.json();
  });
};

export default apiClient;
