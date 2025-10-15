// import { Alert, AlertDescription } from "@/components/ui/alert";
// import { Button } from "@/components/ui/button";
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

import type { ICriaAnalise } from "@/store/analise/interface";
import { useAnaliseStore } from "@/store/analise/store";
import { CheckCircle2, Loader2, Search } from "lucide-react";
import { useEffect } from "react";
//import { CheckCircle2, FileText, Loader2, Search } from "lucide-react";

import { Controller, useForm } from "react-hook-form";
import { useNavigate } from "react-router";

const CriarAnalise = () => {
  const { control, getValues, handleSubmit, watch } = useForm<ICriaAnalise>({
    defaultValues: {
      bairro: "",
      cidade: "",
      cpf: "",
      email: "",
      estado: "",
      logradouro: "",
      nomeCliente: "",
      numero: 0,
      renda: 0,
      telefone: "",
      cep: "",
    },
  });

  const {
    actions: { criarAnalise },
    state: { analiseResponse, loadings },
  } = useAnaliseStore((store) => store);
  const renda = watch("renda");
  const telefone = watch("telefone");
  const cpf = watch("cpf");

  const formatCurrency = (val?: string | number | null) => {
    const str = val == null ? "" : String(val);
    const onlyNumbers = str.replace(/\D/g, "");
    if (!onlyNumbers) return "";

    const floatValue = parseFloat(onlyNumbers) / 100;
    if (isNaN(floatValue)) return "";

    return floatValue.toLocaleString("pt-BR", {
      style: "currency",
      currency: "BRL",
    });
  };

  const formatPhone = (val: string) => {
    const digits = val.replace(/\D/g, "");
    if (digits.length <= 10)
      return digits.replace(/(\d{2})(\d{4})(\d{0,4})/, "($1) $2-$3");
    return digits.replace(/(\d{2})(\d{5})(\d{0,4})/, "($1) $2-$3");
  };

  const formatCPF = (val: string) => {
    const digits = val.replace(/\D/g, "");
    return digits
      .slice(0, 11)
      .replace(/(\d{3})(\d)/, "$1.$2")
      .replace(/(\d{3})(\d)/, "$1.$2")
      .replace(/(\d{3})(\d{1,2})$/, "$1-$2");
  };

  const handleSendSubmit = (data: ICriaAnalise) => {
    criarAnalise(data);
  };

  useEffect(() => {
    
  }, []);

  const navigate = useNavigate();

  return (
    <>
      <Button
        variant={"outline"}
        onClick={() => navigate("consultar")}
        className={
          "bg-slate-900 hover:bg-slate-800 text-white hover:text-gray-300"
        }
      >
        <Search className="mr-2 h-4 w-4" />
        Consultar Proposta
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
            <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
              <div className="space-y-2">
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

              <div className="space-y-2">
                <Label htmlFor="name">Nome Completo</Label>
                <Controller
                  control={control}
                  name="nomeCliente"
                  render={({ field }) => (
                    <Input
                      id="nomeCliente"
                      value={field.value}
                      onChange={field.onChange}
                      placeholder="João da Silva"
                      required
                    />
                  )}
                />
              </div>
            </div>

            <div className="space-y-4 border-t pt-4">
              <h3 className="text-sm font-medium text-slate-900">Endereço</h3>

              <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
                <div className="space-y-2">
                  <Label htmlFor="cep">CEP</Label>
                  <Controller
                    control={control}
                    name="cep"
                    render={({ field }) => (
                      <Input
                        id="cep"
                        value={field.value}
                        onChange={field.onChange}
                        placeholder="00000-000"
                        maxLength={9}
                        required
                      />
                    )}
                  />
                </div>

                <div className="space-y-2 md:col-span-2">
                  <Label htmlFor="street">Rua</Label>
                  <Controller
                    control={control}
                    name="logradouro"
                    render={({ field }) => (
                      <Input
                        id="street"
                        value={field.value}
                        onChange={field.onChange}
                        placeholder="Av. Paulista"
                        required
                      />
                    )}
                  />
                </div>
                <div className="space-y-2 md:col-span-2">
                  <Label htmlFor="street">Cidade</Label>
                  <Controller
                    control={control}
                    name="cidade"
                    render={({ field }) => (
                      <Input
                        id="cidade"
                        value={field.value}
                        onChange={field.onChange}
                        placeholder="Rio de Janeiro"
                        required
                      />
                    )}
                  />
                </div>
              </div>

              <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
                <div className="space-y-2">
                  <Label htmlFor="number">Número</Label>
                  <Controller
                    control={control}
                    name="numero"
                    render={({ field }) => (
                      <Input
                        id="number"
                        value={field.value}
                        onChange={field.onChange}
                        placeholder="123"
                        required
                      />
                    )}
                  />
                </div>

                <div className="space-y-2">
                  <Label htmlFor="neighborhood">Bairro</Label>
                  <Controller
                    name="bairro"
                    control={control}
                    render={({ field }) => (
                      <Input
                        id="neighborhood"
                        value={field.value}
                        onChange={field.onChange}
                        placeholder="Ex: Centro"
                        required
                      />
                    )}
                  />
                </div>

                <div className="space-y-2">
                  <Label htmlFor="state">Estado</Label>
                  <Controller
                    control={control}
                    name="estado"
                    render={({ field }) => (
                      <Input
                        id="state"
                        value={field.value}
                        onChange={field.onChange}
                        placeholder="SP"
                        maxLength={2}
                        required
                      />
                    )}
                  />
                </div>
              </div>
            </div>

            <div className="grid grid-cols-1 md:grid-cols-3 gap-4 border-t pt-4">
              <div className="space-y-2">
                <Label htmlFor="monthly_income">Renda Mensal</Label>
                <Controller
                  control={control}
                  name="renda"
                  render={({ field }) => (
                    <Input
                      id="renda"
                      value={formatCurrency(renda ?? "")}
                      onChange={(e) => {
                        const onlyNumbers = e.target.value.replace(/\D/g, "");
                        const numericValue = parseFloat(onlyNumbers) / 100;
                        field.onChange(isNaN(numericValue) ? 0 : numericValue);
                      }}
                      placeholder="R$ 0,00"
                      inputMode="numeric"
                    />
                  )}
                />
              </div>

              <div className="space-y-2">
                <Label htmlFor="email">Email</Label>
                <Controller
                  name="email"
                  control={control}
                  render={({ field }) => (
                    <Input
                      id="email"
                      type="email"
                      value={field.value}
                      onChange={field.onChange}
                      placeholder="joao@email.com"
                      required
                    />
                  )}
                />
              </div>

              <div className="space-y-2">
                <Label htmlFor="phone">Telefone</Label>
                <Controller
                  name="telefone"
                  control={control}
                  render={({ field }) => (
                    <Input
                      id="telefone"
                      type="text"
                      value={formatPhone(telefone ?? "")}
                      onChange={(e) => {
                        field.onChange(e.target.value.replace(/\D/g, ""));
                      }}
                      placeholder="(41)99999-9999"
                      maxLength={15}
                      required
                    />
                  )}
                />
              </div>
            </div>

            {analiseResponse &&
              analiseResponse.success === false &&
              analiseResponse.errors.length > 0 && (
                <Alert variant="destructive">
                  <AlertDescription>{analiseResponse.errors}</AlertDescription>
                </Alert>
              )}

            {analiseResponse && analiseResponse.success && (
              <Alert className="border-green-200 bg-green-50">
                <CheckCircle2 className="h-4 w-4 text-green-600" />
                <AlertDescription className="text-green-800">
                  proposta enviada com sucesso! sua solicitação está sendo
                  processada.
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
              {loadings.default ? "Processando..." : "Enviar Proposta"}
            </Button>
          </form>
        </CardContent>
      </Card>
    </>
  );
};

export default CriarAnalise;
