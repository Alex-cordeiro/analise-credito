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
import {
  formatCEP,
  formatCPF,
  formatPhone,
  validateCPF,
} from "@/lib/validation";
import type { ICriaAnalise } from "@/store/analise/interface";
import { CheckCircle2, FileText, Loader2, Search } from "lucide-react";
import { useState } from "react";
import { Controller, useForm } from "react-hook-form";

const CriarAnalise = () => {
  const [loading, setLoading] = useState(false);
  const [success, setSuccess] = useState(false);
  const [error, setError] = useState("");

  const { control, getValues, handleSubmit } = useForm<ICriaAnalise>({
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

  const handleInputChange = (field: string, value: string) => {
    let formattedValue = value;

    if (field === "cpf") {
      formattedValue = formatCPF(value);
    } else if (field === "cep") {
      formattedValue = formatCEP(value);
    } else if (field === "phone") {
      formattedValue = formatPhone(value);
    } else if (field === "monthly_income") {
      formattedValue = value.replace(/[^\d]/g, "");
    }
  };

  const handleSendSubmit = async (data: ICriaAnalise) => {
    console.log(data);
  };

  return (
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
                    value={field.value}
                    onChange={(e) => handleInputChange("cpf", e.target.value)}
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
                name="cpf"
                render={({ field }) => (
                  <Input
                    id="name"
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
               render={({field}) => (

                   <Input
                     id="monthly_income"
                     value={
                       formData.monthly_income
                         ? (
                             parseFloat(formData.monthly_income) / 100
                           ).toLocaleString("pt-BR", {
                             style: "currency",
                             currency: "BRL",
                           })
                         : ""
                     }
                     onChange={(e) =>
                       handleInputChange("monthly_income", e.target.value)
                     }
                     placeholder="R$ 0,00"
                     required
                   />
               )}
              />
            </div>

            <div className="space-y-2">
              <Label htmlFor="email">Email</Label>
              <Input
                id="email"
                type="email"
                value={formData.email}
                onChange={(e) => handleInputChange("email", e.target.value)}
                placeholder="joao@email.com"
                required
              />
            </div>

            <div className="space-y-2">
              <Label htmlFor="phone">Telefone</Label>
              <Input
                id="phone"
                value={formData.phone}
                onChange={(e) => handleInputChange("phone", e.target.value)}
                placeholder="(11) 98765-4321"
                maxLength={15}
                required
              />
            </div>
          </div>

          {error && (
            <Alert variant="destructive">
              <AlertDescription>{error}</AlertDescription>
            </Alert>
          )}

          {success && (
            <Alert className="border-green-200 bg-green-50">
              <CheckCircle2 className="h-4 w-4 text-green-600" />
              <AlertDescription className="text-green-800">
                Proposta enviada com sucesso! Sua solicitação está sendo
                processada.
              </AlertDescription>
            </Alert>
          )}

          <Button
            type="submit"
            className="w-full bg-slate-900 hover:bg-slate-800"
            disabled={loading}
          >
            {loading && <Loader2 className="mr-2 h-4 w-4 animate-spin" />}
            {loading ? "Processando..." : "Enviar Proposta"}
          </Button>
        </form>
      </CardContent>
    </Card>
  );
};

export default CriarAnalise;
