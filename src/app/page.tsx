import Solver from './components/solver'

export default function Home() {
  return (
    <main className="flex flex-col items-center lg:px-24 lg:py-8">
      <div className="p-6 max-w-5xl flex">
        <p>Sudoku Solver</p>
      </div>

      <div>
        <Solver />
      </div>
    </main>
  )
}
