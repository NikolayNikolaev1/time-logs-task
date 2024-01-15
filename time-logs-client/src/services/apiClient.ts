const baseUrl = "https://localhost:7052/api";

interface ApiClientProps {
  url: string;
  method: string;
}

const apiClient = async ({ url, method }: ApiClientProps) => {
  return await fetch(`${baseUrl}/${url}`, {
    method,
    mode: "cors",
    headers: {
      "Content-Type": "application/json",
    },
  }).then((response) => {
    if (!response.ok) {
      throw { statusCode: response.status };
    }

    return response.json();
  });
};
