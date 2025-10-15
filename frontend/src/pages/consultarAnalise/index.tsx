import { Alert, AlertDescription } from "@/components/ui/alert";
import { Button } from "@/components/ui/button";
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import type { IPesquisaAnalise } from "@/store/analise/interface";
import { useAnaliseStore } from "@/store/analise/store";
import { CheckCircle2, FileText, Loader2, Search } from "lucide-react";
import { useEffect } from "react";
import { Controller, useForm } from "react-hook-form";
import { useNavigate } from "react-router";

const ConsultarAnalise = () => {
  const navigate = useNavigate();

  const {
    actions: { retornaStatusAnalise, resetResponse },
    state: {
      analiseResponse,
      loadings,
      pesquisaAnalise,
      pesquisaAnaliseResponse,
    },
  } = useAnaliseStore((store) => store);

  useEffect(() => {
    resetResponse();
  }, []);

  const { control, handleSubmit, watch } = useForm<IPesquisaAnalise>({
    defaultValues: {
      cpf: "",
    },
  });

  const cpf = watch("cpf");

  const formatCPF = (val: string) => {
    const digits = val.replace(/\D/g, "");
    return digits
      .slice(0, 11)
      .replace(/(\d{3})(\d)/, "$1.$2")
      .replace(/(\d{3})(\d)/, "$1.$2")
      .replace(/(\d{3})(\d{1,2})$/, "$1-$2");
  };
  const handleSendSubmit = (data: IPesquisaAnalise) => {
    retornaStatusAnalise(data);
  };

  return (
    <>
      <Button
        variant={"outline"}
        onClick={() => navigate("/")}
        className={
          "bg-slate-900 hover:bg-slate-800 text-white hover:text-gray-300"
        }
      >
        <FileText className="mr-2 h-4 w-4" />
        Nova Proposta
      </Button>

      <Card className={`w-full max-w-3xl mx-auto`}>
        <CardHeader>
          <CardTitle className="text-2xl font-semibold text-slate-900">
            Solicitação de Cartão de Crédito
          </CardTitle>
          <CardDescription>
            Preencha os dados abaixo para solicitar seu cartão de crédito
          </CardDescription>
        </CardHeader>
        <CardContent>
          <form onSubmit={handleSubmit(handleSendSubmit)} className="space-y-6">
            <div className="flex justify-center gap-4">
              <div className="space-y-2 w-full text-md">
                <Label htmlFor="cpf">CPF</Label>
                <Controller
                  control={control}
                  name="cpf"
                  render={({ field }) => (
                    <Input
                      id="cpf"
                      value={formatCPF(cpf ?? "")}
                      onChange={(e) =>
                        field.onChange(e.target.value.replace(/\D/g, ""))
                      }
                      placeholder="000.000.000-00"
                      maxLength={14}
                      required
                    />
                  )}
                />
              </div>
            </div>

            {pesquisaAnaliseResponse &&
              pesquisaAnaliseResponse.success === false &&
              pesquisaAnaliseResponse.errors.length > 0 && (
                <Alert variant="destructive">
                  <AlertDescription>{analiseResponse.errors}</AlertDescription>
                </Alert>
              )}

            {pesquisaAnaliseResponse && pesquisaAnaliseResponse.success && (
              <Alert className="border-green-200 bg-green-50">
                <CheckCircle2 className="h-4 w-4 text-green-600" />
                <AlertDescription className="text-green-800">
                  <div className="w-full grid grid-cols-1 gap-4">
                    {pesquisaAnaliseResponse.data?.status === 1 && (
                      <div>
                        Status :{" "}
                        {pesquisaAnaliseResponse.data.analiseStatusDescricao}
                      </div>
                    )}

                    {pesquisaAnaliseResponse.data?.status === 2 && (
                      <>
                        <div>
                          Não foi dessa vez :-(, tente novamente em outro
                          momento, seu score é insuficiente.
                        </div>
                      </>
                    )}

                    {pesquisaAnaliseResponse.data?.status === 3 && (
                      <>
                        <div>
                          Status:{" "}
                          {pesquisaAnaliseResponse.data?.analiseStatusDescricao}
                        </div>
                        <div>
                          Limite Liberado:{" "}
                          {pesquisaAnaliseResponse.data?.limiteLiberado}
                        </div>
                      </>
                    )}
                  </div>
                </AlertDescription>
              </Alert>
            )}

            <Button
              type="submit"
              className="w-full bg-slate-900 hover:bg-slate-800"
              disabled={loadings.default}
            >
              {loadings.default && (
                <Loader2 className="mr-2 h-4 w-4 animate-spin" />
              )}
              {loadings.default ? "Consultando..." : "Consultar"}
            </Button>
          </form>
        </CardContent>
      </Card>
    </>
  );
};

export default ConsultarAnalise;
