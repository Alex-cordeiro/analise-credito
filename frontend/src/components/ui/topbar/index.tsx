import { CreditCard } from "lucide-react";
import type { TopBarProps } from "./ITopBarProps";

const TopBar = ({ subtitle, title }: TopBarProps) => {
  return (
    <div className="w-full bg-slate-900 text-white border-b border-slate-800">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="flex items-center justify-between h-16">
          <div className="flex items-center gap-3">
            <CreditCard className="h-6 w-6 text-slate-300" />
            <div className="flex flex-col">
              <h1 className="text-base font-semibold tracking-tight">
                {title}
              </h1>
              <p className="text-xs text-slate-400">{subtitle}</p>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default TopBar;
